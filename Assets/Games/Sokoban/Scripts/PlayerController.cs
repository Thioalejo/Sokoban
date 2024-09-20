using System.Collections;
using UnityEngine;

namespace Game.Sokoban
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool _isMovementSmooth = false;
        [SerializeField] private float _step = 1;
        [SerializeField] private float _timeMovement = .1f;
        [SerializeField] private LayerMask _layerInteraction;

        //Input
        private SokobanInputAction _inputAction;
        //Movement Smooth  //Se crea una Coroutine para mejorar el movimiento del personaje, y se deplace mas suvamente
        private Coroutine _currentMovement;
        private RaycastHit2D _raycastHitPlayer;
        private RaycastHit2D _raycastHitBox;
        private string _tagBox = "Box";
        private IInteractable _currentBox;


        private void Start()
        {
            _inputAction = new SokobanInputAction();
            if (_isMovementSmooth)
            {
                _inputAction.Player.MoveUp.performed += context => MoveSmooth(Vector2.up);
                _inputAction.Player.MoveDown.performed += context => MoveSmooth(Vector2.down);
                _inputAction.Player.MoveLeft.performed += context => MoveSmooth(Vector2.left);
                _inputAction.Player.MoveRight.performed += context => MoveSmooth(Vector2.right);
            }
            else
            {
                _inputAction.Player.MoveUp.performed += context => MoveBasic(Vector2.up);
                _inputAction.Player.MoveDown.performed += context => MoveBasic(Vector2.down);
                _inputAction.Player.MoveLeft.performed += context => MoveBasic(Vector2.left);
                _inputAction.Player.MoveRight.performed += context => MoveBasic(Vector2.right);
            }


            _inputAction.Player.Enable();
        }

        private void MoveBasic(Vector2 direction)
        {
            _raycastHitPlayer = Physics2D.Raycast(transform.position, direction, _step, _layerInteraction);

            if (_raycastHitPlayer.collider == null)
            {
                transform.position += (Vector3)direction * _step;
            }
            else
            {
                if (_raycastHitPlayer.collider.CompareTag(_tagBox))
                {
                    //para evitar que la caja traspase paredes
                    _raycastHitBox = Physics2D.Raycast(_raycastHitPlayer.transform.position + (Vector3)direction, direction, .1f, _layerInteraction);
                    if (_raycastHitBox.collider == null)
                    {
                        transform.position += (Vector3)direction * _step;
                        _raycastHitPlayer.collider.gameObject.transform.position += ((Vector3)direction * _step);

                        //Para configurar trigger del punto  
                        _currentBox = _raycastHitPlayer.collider.GetComponent<IInteractable>();
                        _currentBox.Trigger();
                    }


                }
            }


        }

        private void MoveSmooth(Vector2 direction)
        {
            if (_currentMovement != null) return;

            _raycastHitPlayer = Physics2D.Raycast(transform.position, direction, _step, _layerInteraction);

            if (_raycastHitPlayer.collider == null)
            {
                _currentMovement = StartCoroutine(PlayerMoveSmooth(direction));
            }
            else
            {
                if (_raycastHitPlayer.collider.CompareTag(_tagBox))
                {
                    //para evitar que la caja traspase paredes
                    _raycastHitBox = Physics2D.Raycast(_raycastHitPlayer.transform.position + (Vector3)direction, direction, .1f, _layerInteraction);
                    if (_raycastHitBox.collider == null)
                    {
                        //se añade el padre a la inteface para la validación de llevar la caja
                        _currentBox = _raycastHitPlayer.collider.GetComponent<IInteractable>();
                        _currentBox?.AddParent(transform);

                        _currentMovement = StartCoroutine(PlayerMoveSmooth(direction));
                    }


                }
            }
        }

        private IEnumerator PlayerMoveSmooth(Vector2 direction)
        {
            //para mover el personaje y modificar el tiempo sumando el tiempo entre frame
            Vector3 starPosition = transform.position;
            Vector3 targetPosition = transform.position + (Vector3)direction;
            float currentTime = 0;

            while (currentTime < _timeMovement)
            {
                transform.position = Vector3.Lerp(starPosition, targetPosition, currentTime / _timeMovement);

                currentTime += Time.deltaTime;
                yield return null;
            }


            transform.position = targetPosition;
            // se valida si tiene un caja como hijo y si es asi se le borra y se deja null vacia
            if (_currentBox != null)
            {
                _currentBox.Trigger();
                _currentBox.RemoveParent();
                _currentBox = null;
            }

            _currentMovement = null;

        }

        private void OnDestroy()
        {
            _inputAction.Player.Disable();
        }
    }

}



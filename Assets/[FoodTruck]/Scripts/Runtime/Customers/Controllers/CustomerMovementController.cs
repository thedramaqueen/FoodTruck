using UnityEngine;
using UnityEngine.AI;

public class CustomerMovementController : MonoBehaviour
{
    public Vector3 target;

    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _movementSpeed;

    public bool isStopped;
    public bool isEmpty = false;

    private CustomerAnimationController _customerAnimation;
    private CustomerAnimationController CustomerAnimation => _customerAnimation == null ? _customerAnimation = GetComponent<CustomerAnimationController>() : _customerAnimation;

    private void Update()
    {
        if (!isStopped)
            MoveTarget();
    }

    private void LateUpdate()
    {
        if (!isStopped)
        {
            if (Vector3.Distance(transform.position, target) <= 0.5f)
            {
                isStopped = true;
                StopMovement();
            }
        }
    }

    private void MoveTarget()
    {
        _navMeshAgent.SetDestination(target);
        _navMeshAgent.speed = _movementSpeed;

        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 dir = target - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.GetChild(0).rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.GetChild(0).rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    public void StartMovement()
    {
        _navMeshAgent.isStopped = false;
        isStopped = false;
    }

    public void StopMovement()
    {
        _navMeshAgent.isStopped = true;
        CustomerAnimation.ChangeAnimation(CustomerAnimation.IDLE_ANIM);

        if (!isEmpty)
            ChefManager.Instance.SendAvailableChef(GetComponent<Customer>());
            //chefOrderController.Instance.AddCustomer(GetComponent<Customer>());
                
    }
}

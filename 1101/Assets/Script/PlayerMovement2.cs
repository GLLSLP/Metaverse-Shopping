using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Range(0f, 1f)]
    public float maxLength;

    public LayerMask groundedLayer;
    private enum SetMoveType
    {
        Direct,
        LookCam,
        BackWalk
    }
    [SerializeField] private SetMoveType setMoveType = SetMoveType.Direct; // �� ������ Ÿ��
    [SerializeField] private float moveSpeed = 4f; // ĳ���� �̵� �ӵ�
    [SerializeField] private float jumpFoce =  5f; // ĳ���� ���� 
    [SerializeField] private float turnSpeed = 200f; // ĳ���� ȸ�� �ӵ�
    [SerializeField] private bool isGrounded = false; // �� üũ

    private readonly float walkScale = 0.33f; // �ȴ� �ӵ� ����
    private readonly float backWalkScale = 0.55f; // �ڷ� �ȴ� �ӵ� ����
    private readonly float backRunScale = 0.88f; // �ڷ� �ٴ� �ӵ� ����
    private readonly float interpolation = 10f; // �׷��� ����

    private float currentV = 0; // ���� Vertical ��
    private float currentH = 0; // ���� Horizontal ��
    private Vector3 currentDirection = Vector3.zero; // ���� ���� ���� ����

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ���� ��Ҵ� ��?
        CheckGround();
    }


    private void FixedUpdate()
    {
        // �� Ÿ�Կ� �´� 
        switch (setMoveType)
        {
            case SetMoveType.Direct:
                DirectMove();
                break;
            case SetMoveType.LookCam:
                LookCamMove();
                break;
            case SetMoveType.BackWalk:
                BackWalkMove();
                break;
        }
    }

    private void DirectMove()
    {
        float v = playerInput.verticalInput;
        float h = playerInput.horizontalInput;

        // �޸��⸦ ���� ������ �ӵ� ����
        if (!playerInput.runInput)
        {
            v *= walkScale;
            h *= walkScale;
        }

        // �ε巯�� �̵� �ӵ� ������ ���� ����
        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        // ���� ���� ��
        // - �ش� ������ ������ ��
        Vector3 direction = new Vector3(currentH, 0, currentV);

        // �̵� �� �̶��
        if (direction != Vector3.zero)
        {
            // �ε巯�� ȸ���� ���� ����
            currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);

            // ĳ������ ������ �־���
            transform.forward = currentDirection;

            // Rigidbody�� MovePosition �޼ҵ�� ĳ���� �̵�
            // - transfrom.position�� ����ص� ������ ���� ������ ����� �������� ���� �� �ִ�.
            // - ��ü�� �浹�� �����ϸ鼭 �̵��ϱ� ���� MovePosition�� ��� �ߴ�.
            playerRigidbody.MovePosition(playerRigidbody.position + currentDirection * Time.deltaTime * moveSpeed);

            // currentDirection�� Vector�� �̱� ������ magnitude�� ���̸� ��ȯ�Ѵ�.
            // playerInput�� �ִ밪�� 1�̱� ������ currentDirection ���� �ִ� �� 1�� ��ȯ��.
            playerAnimator.SetFloat("MoveSpeed", currentDirection.magnitude);
        }

        // ���� ����
        Jump();
    }

    private void LookCamMove()
    {
        float v = playerInput.verticalInput;
        float h = playerInput.horizontalInput;

        Transform mainCam = Camera.main.transform;

        if (!playerInput.runInput)
        {
            v *= walkScale;
            h *= walkScale;
        }

        // �ε巯�� �̵� �ӵ� ������ ���� ����
        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        // ���� ������ ī�޶��� �������� ����
        Vector3 direction = mainCam.forward * currentV + mainCam.right * currentH;

        // y���� ����
        direction.y = 0;

        // �������� �ִٸ�
        if (direction != Vector3.zero)
        {
            // �ε巯�� ȸ���� ���� ����
            currentDirection = Vector3.Slerp(currentDirection, direction, Time.deltaTime * interpolation);

            // ĳ������ ���� ����
            transform.forward = currentDirection;

            // ĳ������ ������
            playerRigidbody.MovePosition(playerRigidbody.position + currentDirection * Time.deltaTime * moveSpeed);

            // ĳ������ �ִϸ��̼�
            playerAnimator.SetFloat("MoveSpeed", currentDirection.magnitude);
        }

        Jump();
    }

    private void BackWalkMove()
    {
        float v = playerInput.verticalInput;
        float h = playerInput.horizontalInput;
        bool run = playerInput.runInput; // �ȴ��� Ȯ���� ���� ����

        // ���� ���� �Է��� �� ���� �̸�
        // - v�� ������ ĳ���� ������ �ݴ� �����̴�.
        if (v < 0)
        {
            // �ȴ� �� �ٴ� �� Ȯ��
            if (!run)
            {
                // �ڷ� �Ȱ� �ִٸ� �ӵ��� ����
                v *= backWalkScale;
            }
            else
            {
                // �ڷ� �ٰ� �ִٸ� �ӵ��� ����
                v *= backRunScale;
            }
        }
        // ���� �ڷ� ���� �ʰ� ������ �Ȱ� ���� ���
        // �ٰ� ���� �ʴٸ� �ȴ� �ӵ� ����
        else if (!run)
        {
            v *= walkScale;
        }

        // ���ǵ带 ���� ���ְ�
        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);

        // �÷��̾� ������
        // (transform.forward * currentV * moveSpeed * Time.deltaTime)
        // �÷��̾��� ���� ���� * ���� �Է� �� * ������ �� * ������ ����
        playerRigidbody.MovePosition(transform.position +
                                    (transform.forward * currentV * moveSpeed * Time.deltaTime));

        // �÷��̾� y���� �������� ȸ��
        // (0, currentH * turnSpeed * Time.deltaTime, 0)
        // (0, ���� �Է� �� * ȸ�� �� * ������ ����, 0 )
        transform.Rotate(0, currentH * turnSpeed * Time.deltaTime, 0);

        playerAnimator.SetFloat("MoveSpeed", currentV);

        Jump();
    }

    private void Jump()
    {
        // ����Ű�� �Է� �����鼭 ���� ��� ���� ���
        if (playerInput.jumpInput && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpFoce, ForceMode.Impulse);
            playerAnimator.SetBool("Jump", playerInput.jumpInput);
        }
    }

    private void CheckGround()
    {
#if UNITY_EDITOR
        Debug.DrawRay(transform.position + new Vector3(0, 0.05f, 0), Vector3.down * maxLength, Color.red);
#endif
        // �÷��̾��� ���� ���� ���� �Ʒ��� 0.2f ��ŭ ���̸� �߻�
        // ���̸���ũ�� ���� �ش� ���̿��� ����
        if (Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector3.down, maxLength, groundedLayer))
        {
            // ���� �ִٸ�
            Debug.Log("����!");
            isGrounded = false;
        }
        else
        {
            // �˻簡 �ȵǸ�
            Debug.Log("�������� ����!");
            isGrounded = true;
        }
    }

    
}
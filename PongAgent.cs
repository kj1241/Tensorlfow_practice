
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PongAgent : Agent
{
    public GameObject Ball;
    public GameObject Opponent;
    private Rigidbody RbBall;
    private Rigidbody RbAgent;
    private Rigidbody RbOpponent;
    private const int Stay = 0;
    private const int Up = 1;
    private const int Down = 2;
    Vector3 ResetPos;


    void Start()
    {
        RbAgent = GetComponent<Rigidbody>();
        RbOpponent = Opponent.GetComponent<Rigidbody>();
        RbBall = Ball.GetComponent<Rigidbody>();
        //�ڽ��� �⺻ ������ ����
        ResetPos = transform.position;
    }
   /*
    private void Update()
    {
        float a =Input.GetAxis("Vertical");
        Vector3 position = transform.position;
        position.x += a * Time.deltaTime*5;
        transform.position = position;
    }
   */
    //���� ����.
    public override void CollectObservations(VectorSensor sensor)
    {
        //�Ű��
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);

        sensor.AddObservation(Opponent.transform.position.x);
        sensor.AddObservation(Opponent.transform.position.z);

        sensor.AddObservation(Ball.transform.position.x);
        sensor.AddObservation(Ball.transform.position.z);

        sensor.AddObservation(RbAgent.velocity.x);
        sensor.AddObservation(RbAgent.velocity.z);

        sensor.AddObservation(RbBall.velocity.x);
        sensor.AddObservation(RbBall.velocity.z);

    }

    public override void OnActionReceived(float[] vectorAction)
    {
      
        
        int action = Mathf.FloorToInt(vectorAction[0]);
        switch(action)
        {
            case Stay:
                //������Ʈ�� ���� ��ǥ�� �״�� ����
                this.transform.position = this.transform.position + 0f * Vector3.right;
                break;

            case Up:
                //���� ��ǥ�� ����Ƽ ��ǥ�� �����ʸ�ŭ �̵�
                this.transform.position = this.transform.position + 0.3f * Vector3.right;
                break;

            case Down:
                //���� ��ǥ�� ����Ƽ ��ǥ�� �����ʸ�ŭ �̵�
                this.transform.position = this.transform.position + 0.3f * Vector3.left;
                break;

        }
        



    }

    public override void OnEpisodeBegin()
    {
        //�ʱ�ȭ �ϵ� �� �����غ���.
        transform.position = ResetPos;
        // Ball.GetComponent<PongGoalDetection>().ResetPosition();


    }

    public void OpponenwtScored()
    {
        AddReward(-1f);
        EndEpisode();
    }
    public void ScoredGoal()
    {
        AddReward(1f);
        EndEpisode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent("Wall"))
        {
            RbAgent.velocity = Vector3.zero;
            RbAgent.angularVelocity = Vector3.zero;
            
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            AddReward(0.5f);
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        // �׽�Ʈ�Ҷ� �ʿ��� ���̱� ����
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            actionsOut[0] = RbAgent.position.z + 1f;

            Debug.Log("a");
        }
      */
       

    }





}

using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public PressurePlateSystem Instance { get; set; }
    public bool activated { get; private set; }
    [SerializeField] private GameObject crate;
    [SerializeField] private float acceptableDistance = 0.25f;

    private void Start()
    {
        if (crate != null && SaveManager.Instance.GetFlag(SaveData.Flag.Crate, this.name))
        {
            var from = crate.transform.position;
            var to = this.transform.position;
            crate.transform.position = new Vector3(to.x, to.y, from.z);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (activated) return;

        if (Vector3.Distance(transform.position,other.transform.position ) <= acceptableDistance)
        {
            if (other.gameObject == crate)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                activated = true;
                Instance.Check();
                SaveManager.Instance.SetFlag(SaveData.Flag.Crate, this.name);
            }
            else
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
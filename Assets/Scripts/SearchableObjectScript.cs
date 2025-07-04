using UnityEngine;

// ���ׂ���I�u�W�F�N�g�ɐ����o��UI��\���E��\������X�N���v�g
public class SearchableObjectScript : MonoBehaviour
{
    private GameObject currentBubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowBubble(GameObject bubblePrefab)
    {
        if (currentBubble == null)
        {
            currentBubble = Instantiate(bubblePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            currentBubble.transform.SetParent(transform);
        }
    }

    public void HideBubble()
    {
        if (currentBubble != null)
        {
            Destroy(currentBubble);
            currentBubble = null;
        }
    }
}

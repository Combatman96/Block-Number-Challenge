using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private GridMap m_gridMap;
    [SerializeField] private LayerMask m_nodeLayerMask;

    private Node m_rootNode;
    private Node m_endNode;

    // Start is called before the first frame update
    void Start()
    {
        m_gridMap.DoStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, 1000f, m_nodeLayerMask);
            if (isHit)
            {
                var node = hit.transform.GetComponent<Node>();
                m_rootNode = (node.IsRoot()) ? node : null;
            }
        }
        if (Input.GetMouseButtonUp(0) && m_rootNode)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, 1000f, m_nodeLayerMask);
            if (isHit)
            {
                m_endNode = hit.transform.GetComponent<Node>();
                if (m_endNode != m_rootNode)
                {
                    m_gridMap.Spawn(m_rootNode, m_endNode);
                }
            }
            m_endNode = null;
            m_rootNode = null;
        }
    }
}

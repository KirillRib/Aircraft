using UnityEngine;
using System.Collections;

namespace DeadRoad
{

    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class SetSortingLayer : MonoBehaviour
    {

        public string LayerName = "Default";
        public int Order;

        void Start()
        {
            Apply();
        }

        public void Set(string layerName, int order)
        {
            LayerName = layerName;
            Order = order;

            Apply();
        }

        void Apply()
        {
            GetComponent<Renderer>().sortingLayerName = LayerName;
            GetComponent<Renderer>().sortingOrder = Order;
        }
    }
}


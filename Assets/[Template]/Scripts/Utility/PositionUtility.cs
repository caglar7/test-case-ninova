using System.Collections.Generic;
using Template.ObjectGridModule;
using UnityEngine;

namespace Template
{
    public static class PositionUtility
    {
        public static Vector3 GetAveragePosition<T>(List<T> types) where T : MonoBehaviour
        {
            Vector3 pos = Vector3.zero;
            foreach (T type in types)
            {
                if (type is BaseMono baseMono)
                {
                    pos += baseMono.transform.position;
                }
            }
            return pos / types.Count;
        }
        
        public static void CenteredLocalX(List<BaseMono> objs, float distanceX)
        {
            int count = objs.Count;
            
            float distanceToLeftMost = (count - 1) * (distanceX / 2f);
            
            Vector3 localPos = new Vector3(-distanceToLeftMost, 0f, 0f);
            
            for (int i = 0; i < objs.Count; i++)
            {
                objs[i].Transform.localPosition = localPos;
                localPos.x += distanceX;
            }
        }

        public static Vector3 GetGridLocalPosition(
            ObjectGrid objectGrid,
            int rowIndex,
            int columnIndex)
        {
            Vector3 topLeftPos = Vector3.zero;
            topLeftPos.x = -1 * (objectGrid.rowCount - 1) * 0.5f * objectGrid.unitDistanceX;
            topLeftPos.y = 1 * (objectGrid.columnCount - 1) * 0.5f * objectGrid.unitDistanceY;
            Vector3 targetLocalPos = topLeftPos;

            targetLocalPos.x += ((rowIndex + 1) * objectGrid.unitDistanceX);
            targetLocalPos.y -= ((columnIndex + 1) * objectGrid.unitDistanceY);
            
            return targetLocalPos;
        }
    }
}
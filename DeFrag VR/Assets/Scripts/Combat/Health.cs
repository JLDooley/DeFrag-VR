using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

namespace Game.Combat
{
    public class Health : MonoBehaviour
    {
        public IntReference maxHealth;

        public IntReference health;



        /// <summary>
        /// Change Health by a set amount
        /// </summary>
        /// <param name="changeAmount">Value to change by (sign required).</param>
        public void ChangeHealth(int changeAmount)
        {
            if (health.UseConstant)
            {
                health.ConstantValue += changeAmount;
                if (health.Value > maxHealth.Value)
                {

                }
            }
            else
            {
                health.Variable.Value += changeAmount;
            }
        }
    }
}


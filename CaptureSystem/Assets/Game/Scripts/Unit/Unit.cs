// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Rigidbody _rb = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody UnitRigidBody => _rb;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class DoorTrigger : InteractableBase
    {
        [SerializeField] private GameObject _doorLeft;
        [SerializeField] private GameObject _doorRight;

        private Vector3 _doorLeftOpenPos;
        private Vector3 _doorRightOpenPos;

        private void Awake()
        {
            _doorLeftOpenPos = new Vector3(25.775f, -1, 5.654f);
            _doorRightOpenPos = new Vector3(30f, -1, 1.44f);
        }

        public override void  OnInteract()
        {
            base.OnInteract();

            StartCoroutine(DoorOpen());
        }

        private IEnumerator DoorOpen()
        {
            while (Vector3.Distance(_doorLeft.transform.localPosition, _doorLeftOpenPos) > 0.1f)
            {
                _doorLeft.transform.localPosition = Vector3.Lerp(_doorLeft.transform.localPosition, _doorLeftOpenPos, Time.deltaTime * 2);
                _doorRight.transform.localPosition = Vector3.Lerp(_doorRight.transform.localPosition, _doorRightOpenPos, Time.deltaTime * 2);
                yield return null;
            }

            Destroy(this);
        }
    }
}

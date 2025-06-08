using UnityEngine;

namespace NJG.Prototypes
{
    public class SpritePawn : PlayerPawn
    {
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        
        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        public override bool GoTo(Vector3 targetPosition)
        {
            bool success = base.GoTo(targetPosition);
            if (!success) { return false; }

            Vector3 point = Controller.GetCamera().WorldToScreenPoint(targetPosition);
            Vector3 me = Controller.GetCamera().WorldToScreenPoint(transform.position);
            _spriteRenderer.flipX = point.x < me.x;
            return true;
        }
    }
}
using BIS.Animators;
using BIS.Init;
using UnityEngine;

namespace BIS.Entities
{
    public class EntityRenderer : AnimateRenderer, IEntityComponentInit
    {
        protected Entity _entity;
        [field: SerializeField] public float FacingDirection { get; private set; } = 1;
       public SpriteRenderer Renderer { get; private set; }
        public void Initalize(Entity entity)
        {
            _entity = entity;
            Renderer = GetComponent<SpriteRenderer>();
        }

        #region FlipController

        public void Flip()
        {
            FacingDirection *= -1;
            _entity.transform.Rotate(0, 180f, 0);
        }

        public void FlipController(float normalizeXMove)
        {
            if (Mathf.Abs(FacingDirection + normalizeXMove) < 0.5f)
                Flip();
        }

        #endregion
    }
}
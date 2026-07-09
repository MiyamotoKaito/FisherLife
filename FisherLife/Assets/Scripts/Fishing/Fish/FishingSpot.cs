using UnityEngine;

namespace FishModule
{
    /// <summary>
    ///     魚が集まるスポット。魚の生成中心と水しぶきVFXを持つ。
    /// </summary>
    public class FishingSpot : MonoBehaviour
    {
        [SerializeField, Tooltip("投げた瞬間の水しぶき(一回)。")]
        private ParticleSystem _throwSplashVfx;
        [SerializeField, Tooltip("戦闘中のループVFX。")]
        private ParticleSystem _battleLoopVfx;

        /// <summary> 魚の生成中心。 </summary>
        public Vector3 Position => transform.position;

        /// <summary> 投げた方向へY回転を合わせる（水平のみ）。 </summary>
        public void AimTo(Vector3 direction)
        {
            var flat = new Vector3(direction.x, 0f, direction.z);
            if (flat.sqrMagnitude < 0.0001f) return;
            transform.rotation = Quaternion.LookRotation(flat);
        }

        /// <summary> 投げた瞬間の水しぶきを一回再生する。 </summary>
        public void PlaySplash()
        {
            if (_throwSplashVfx != null) _throwSplashVfx.Play();
        }

        /// <summary> 戦闘中のループVFXを開始する。 </summary>
        public void StartBattleVfx()
        {
            if (_battleLoopVfx != null) _battleLoopVfx.Play();
        }

        /// <summary> 戦闘中のループVFXを止める。 </summary>
        public void StopBattleVfx()
        {
            if (_battleLoopVfx != null) _battleLoopVfx.Stop();
        }

        private void Start()
        {
            _battleLoopVfx.Stop();
            _throwSplashVfx.Stop();
        }
    }
}

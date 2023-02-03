﻿using ArsonistMod.Modules;
using ArsonistMod.Modules.Networking;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;

namespace ArsonistMod.SkillStates.Arsonist.Secondary
{
    internal class FlareEffectControllerWeakWorld : MonoBehaviour
    {
        public CharacterBody arsonistBody;
        public GameObject effectObj;
        public GameObject gameObj;
        public Vector3 worldPos;
        private int timesFired;
        private float timer;

        void Start()
        {
            timer = 0f;
            timesFired = 0;
            gameObj = gameObject.GetComponent<GameObject>();


            Vector3 randVec = new Vector3((float)Random.Range(-2, 2), (float)Random.Range(-2, 2), (float)Random.Range(-2, 2));
            effectObj = Object.Instantiate<GameObject>(Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("flareAttached"), worldPos, Quaternion.LookRotation(worldPos));

        }

        void FixedUpdate()
        {
            if (timer > Modules.StaticValues.flareInterval)
            {
                if (timesFired < Modules.StaticValues.flareTickNum)
                {
                    timesFired++;
                    timer = 0;
                    FireExplosion(Modules.StaticValues.flareBlastRadius / 2f);
                    AkSoundEngine.PostEvent(3747272580, gameObj);
                }
                else
                {
                    FireExplosion(Modules.StaticValues.flareBlastRadius);
                    AkSoundEngine.PostEvent(3061346618, gameObj);

                    EffectManager.SpawnEffect(Modules.Assets.explosionPrefab, new EffectData
                    {
                        origin = worldPos,
                        scale = StaticValues.flareBlastRadius,
                        rotation = new Quaternion(0, 0, 0, 0)
                    }, true);
                    Destroy(this);
                    Destroy(effectObj);
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }


        private void FireExplosion(float radius)
        {
            BlastAttack blastAttack;
            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = 1f;
            blastAttack.teamIndex = TeamIndex.Player;
            blastAttack.position = worldPos;
            blastAttack.attacker = arsonistBody.gameObject;
            blastAttack.crit = arsonistBody.RollCrit();

            blastAttack.baseDamage = this.arsonistBody.baseDamage * Modules.StaticValues.flareWeakDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = 1f;
            blastAttack.damageType = DamageType.Generic;

            blastAttack.Fire();

        }
        private void OnDestroy()
        {
            Destroy(effectObj);
        }
    }
}
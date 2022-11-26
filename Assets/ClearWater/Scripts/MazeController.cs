using System.Collections.Generic;
using Obi;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.ClearWater.Scripts
{
    public class MazeController : MonoBehaviour
    {
        [System.Serializable]
        public class ScoreChangedEvent : UnityEvent<int, int> { }

        public ObiSolver solver;
        public ObiEmitter emitter;
        public FluidColorizer[] colorizers;
        public ObiCollider finishLine;

        public float angularAcceleration = 5;

        [Range(0, 1)]
        public float angularDrag = 0.2f;

        public LevelController levelController;

        HashSet<int> finishedParticles = new HashSet<int>();
        HashSet<int> coloredParticles = new HashSet<int>();

        float angularSpeed = 0;
        float angle = 0;

        // Start is called before the first frame update
        void Start()
        {
            solver.OnCollision += Solver_OnCollision;
            emitter.OnEmitParticle += Emitter_OnEmitParticle;
        }

        private void OnDestroy()
        {
            solver.OnCollision -= Solver_OnCollision;
            emitter.OnEmitParticle -= Emitter_OnEmitParticle;
        }

        void Update()
        {
            var keyboard = Keyboard.current;

            if (keyboard == null)
                return;

            if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
            {
                angularSpeed += angularAcceleration * Time.deltaTime;
            }
            if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
            {
                angularSpeed -= angularAcceleration * Time.deltaTime;
            }
            angularSpeed *= Mathf.Pow(1 - angularDrag, Time.deltaTime);
            angle += angularSpeed * Time.deltaTime;

            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

            if (keyboard.rKey.isPressed)
            {
                Restart();
            }
        }

        public void Restart()
        {
            transform.rotation = Quaternion.identity;
            angularSpeed = angle = 0;
            finishedParticles.Clear();
            coloredParticles.Clear();
            levelController.UpdateScore(finishedParticles.Count, coloredParticles.Count);
            emitter.KillAll();
        }

        void Emitter_OnEmitParticle(ObiEmitter em, int particleIndex)
        {
            int k = emitter.solverIndices[particleIndex];
            solver.userData[k] = solver.colors[k];
        }

        private void Solver_OnCollision(ObiSolver s, ObiSolver.ObiCollisionEventArgs e)
        {
            var world = ObiColliderWorld.GetInstance();
            foreach (Oni.Contact contact in e.contacts)
            {
                // look for actual contacts only:
                if (contact.distance < 0.01f)
                {
                    var col = world.colliderHandles[contact.bodyB].owner;
                    if (colorizers[0].collider == col)
                    {
                        solver.userData[contact.bodyA] = colorizers[0].color;
                        if (coloredParticles.Add(contact.bodyA))
                            levelController.UpdateScore(finishedParticles.Count, coloredParticles.Count);
                    }
                    else if (colorizers[1].collider == col)
                    {
                        solver.userData[contact.bodyA] = colorizers[1].color;
                        if (coloredParticles.Add(contact.bodyA))
                            levelController.UpdateScore(finishedParticles.Count, coloredParticles.Count);
                    }
                    else if (finishLine == col)
                    {
                        if (finishedParticles.Add(contact.bodyA))
                            levelController.UpdateScore(finishedParticles.Count, coloredParticles.Count);
                    }
                }
            }
        }

        void LateUpdate()
        {
            for (int i = 0; i < emitter.solverIndices.Length; ++i)
            {
                int k = emitter.solverIndices[i];
                emitter.solver.colors[k] = emitter.solver.userData[k];
            }
        }
    }
}

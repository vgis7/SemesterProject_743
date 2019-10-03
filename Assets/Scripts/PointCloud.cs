using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloud : MonoBehaviour{
    public SceneSettings sceneSettings;
    ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;
    private List<Vector4> customData = new List<Vector4>();

    void Start(){
        InitializeParticleSystem();
    }

    void Update(){
        
    }

    private void InitializeParticleSystem(){
        particleSystem = this.transform.Find("ParticleSystem").GetComponent<ParticleSystem>();
        particleSystem.maxParticles = 2000;
    }

    public void SetAllParticlesPositions(Vector3[] rayHitPositions){
        if(particleSystem.particleCount == 0){
            particles = new ParticleSystem.Particle[rayHitPositions.Length];
            particleSystem.emission.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(0.0f,(Screen.width/sceneSettings.pixelsEachRayCovers)*(Screen.height/sceneSettings.pixelsEachRayCovers))
            });
        }
        
        int particlesAlive = particleSystem.GetParticles(particles);

        for(int i = 0; i < particlesAlive; i++){
            particles[i].position = rayHitPositions[i];
            particles[i].remainingLifetime = 100;
        }

        particleSystem.SetParticles(particles,particles.Length);
    }
}

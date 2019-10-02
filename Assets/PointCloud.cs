using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloud : MonoBehaviour{
    public SceneSettings sceneSettings;
    ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    void Start(){
        InitializeParticleSystem();
    }

    void Update(){
        
    }

    private void InitializeParticleSystem(){
        particleSystem = this.transform.Find("ParticleSystem").GetComponent<ParticleSystem>();
        particleSystem.maxParticles = 2000;
        /*particleSystem.emission.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(0.0f,(Screen.width/sceneSettings.rayDivideByScreenSize)*(Screen.height/sceneSettings.rayDivideByScreenSize))
            });*/
    }

    public void SetAllParticlesPositions(Vector3[] rayHitPositions){
        if(particleSystem.particleCount == 0){
            particles = new ParticleSystem.Particle[rayHitPositions.Length];
        }

        for(int i = 0; i < particles.Length;i++){
            particles[i].position = rayHitPositions[i];
        }
    }

}

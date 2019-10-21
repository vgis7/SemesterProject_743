using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloud : MonoBehaviour{
    public SceneSettings sceneSettings;
    public ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;
    private List<Vector4> customData = new List<Vector4>();

    public bool pointCloudIsReady;

    void Start(){
        pointCloudIsReady = false;
    }

    private void Update(){
        CheckIfCloudIsReady();
    }

    /// <summary>
    /// Checks if the pointcloud is ready. This is used for beginning taking screenshots in PicoFlexxSensor.
    /// </summary>
    private void CheckIfCloudIsReady(){
        if (particleSystem.particleCount > 0) {
            pointCloudIsReady = true;
        }
    }

    /// <summary>
    /// Takes the positions of where each ray hits a collider as parameter and then translates each particle to that position.
    /// Called from PicoFlexxSensor.
    /// </summary>
    /// <param name="rayHitPositions"></param>
    public void SetAllParticlesPositions(Vector3[] rayHitPositions){
        if(particleSystem.particleCount == 0){
            particles = new ParticleSystem.Particle[rayHitPositions.Length];
            particleSystem.emission.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(0.0f,(sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers)*(sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers ))
            });
        }

        int particlesAlive = particleSystem.GetParticles(particles);
        for (int i = 0; i < particlesAlive; i++){
            particles[i].position = rayHitPositions[i];
            particles[i].remainingLifetime = 100;
        }

        particleSystem.SetParticles(particles,particles.Length);
    }

    struct Point{
        private Vector3 direction;
        private float distance;
    }

    /// <summary>
    /// Takes the origin of the camera, direction of where each ray and distance from camera to hit as parameter and then translates each particle to that position.
    /// Called from PicoFlexxSensor.
    /// </summary>
    /// <param name="rayHitPositions"></param>
    public void SetAllParticlesPositions(Vector3 cameraOrigin,Vector3[] direction,float[] distance){
        if(particleSystem.particleCount == 0){
            particles = new ParticleSystem.Particle[direction.Length];
            particleSystem.emission.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(0.0f,(sceneSettings.imageWidth/sceneSettings.pixelsEachRayCovers)*(sceneSettings.imageHeight/sceneSettings.pixelsEachRayCovers ))
            });
        }

        int particlesAlive = particleSystem.GetParticles(particles);
        for (int i = 0; i < particlesAlive; i++){
            if(direction[i] == new Vector3(0,0,0)){
                particles[i].position = new Vector3(0,10,0);
                continue;
            }

            particles[i].position = cameraOrigin+direction[i]*(distance[i]*Random.Range(0.9f,1.1f));


            if (particles[i].position == new Vector3(0, 0, 0)) {
                particles[i].position = new Vector3(0, 10, 0);
            }

            particles[i].remainingLifetime = 100;
        }
        particleSystem.SetParticles(particles,particles.Length);
    }
}

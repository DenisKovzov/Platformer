using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer
{
    public class EntryPoint : MonoBehaviour
    {

        [SerializeField] private Player player;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private Environment environment;
        [SerializeField] private EnvironmentConfig environmentConfig;



        private void Awake()
        {
            IPlayerInput playerInput = GetPlayerInput();
            player.Construct(playerInput, playerConfig.config);
            player.Initialize();

            cameraController.Construct(player.transform);

            environment.waterList.ForEach(e =>
            {
                e.Construct(environmentConfig.water);
                e.Initialize();
            });

        }


        private IPlayerInput GetPlayerInput()
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "Input";

            return gameObject.AddComponent<DekstopPlayerInput>();

        }
    }

    [Serializable]
    public class Environment
    {
        public List<DamageDealer> waterList;
    }
}

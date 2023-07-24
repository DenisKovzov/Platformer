using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private PlayerConfig config;


        private void Awake()
        {
            IPlayerInput playerInput = GetPlayerInput();
            player.Construct(playerInput, config);

            player.Initialize();
        }


        private IPlayerInput GetPlayerInput()
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "Input";

            return gameObject.AddComponent<DekstopPlayerInput>();

        }
    }
}

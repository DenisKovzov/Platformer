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
        [SerializeField] private HealthBarUI healthBar;
        [SerializeField] private Transform startPoint;
        [SerializeField] private PauseMenuUI pauseMenu;
        [SerializeField] private WonScreenUI wonScreenUI;
        [SerializeField] private LoseScreenUI loseScreenUI;

        private GameStateMachine stateMachine = new GameStateMachine();


        private void Awake()
        {
            IPlayerInput playerInput = GetPlayerInput();
            player.Construct(playerInput, playerConfig.config);
            player.Initialize();

            healthBar.Construct(player);

            cameraController.Construct(player.transform);

            environment.waterList.ForEach(e =>
            {
                e.Construct(environmentConfig.water);
                e.Initialize();
            });

            Dictionary<Type, State> states = new Dictionary<Type, State>();
            states.Add(typeof(InitializeState), new InitializeState(stateMachine, startPoint.position, player, new List<IResetable>()));
            states.Add(typeof(GameplayState), new GameplayState(stateMachine, playerInput));
            states.Add(typeof(PauseState), new PauseState(stateMachine, playerInput, new List<IPauseable>(), pauseMenu));
            states.Add(typeof(EndGameState), new EndGameState(stateMachine, wonScreenUI, loseScreenUI));

            stateMachine.AddStates(states);

            stateMachine.EnterIn<InitializeState>();
        }


        private void Update()
        {
            stateMachine.Update();
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

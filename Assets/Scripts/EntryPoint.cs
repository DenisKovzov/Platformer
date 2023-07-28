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
            PlayerInputController playerInputController = GetPlayerInputController();
            player.Construct(playerInputController, playerConfig.config);
            player.Initialize();

            healthBar.Construct(player);

            cameraController.Construct(player.transform);

            environment.waterList.ForEach(e =>
            {
                e.Construct(environmentConfig.water);
                e.Initialize();
            });

            pauseMenu.Construct(stateMachine);
            loseScreenUI.Construct(stateMachine);

            pauseMenu.Hide();
            wonScreenUI.Hide();
            loseScreenUI.Hide();

            Dictionary<Type, State> states = new Dictionary<Type, State>();
            states.Add(typeof(StartGameState), new StartGameState(stateMachine, startPoint.position, player, new List<IResetable>()));
            states.Add(typeof(GameplayState), new GameplayState(stateMachine, playerInputController));
            states.Add(typeof(PauseState), new PauseState(stateMachine, playerInputController, new List<IPauseable>() { playerInputController }, pauseMenu));
            states.Add(typeof(EndGameState), new EndGameState(stateMachine, wonScreenUI, loseScreenUI));
            stateMachine.AddStates(states);

            stateMachine.EnterIn<StartGameState>();
        }


        private void Update()
        {
            stateMachine.Update();
        }


        private PlayerInputController GetPlayerInputController()
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "Input";

            PlayerInputController inputController = gameObject.AddComponent<PlayerInputController>();
            inputController.Construct(new DekstopPlayerInput());

            return inputController;

        }

    }

    [Serializable]
    public class Environment
    {
        public List<DamageDealer> waterList;
    }
}

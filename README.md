# ML-Agents Experiments

This repository contains experiments and projects using the ML-Agents toolkit by Unity Technologies.

## Installation

Please refer to the [official ML-Agents documentation](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Installation.md) for installation instructions.

## Requirements

The following dependencies are required to run the projects in this repository:

- mlagents==0.29.0
- mlagents-envs==0.29.0
- numpy==1.21.2
- torch==1.7.1+cu110

If you are unable to install torch using pip due to its size, you can download it from [here](https://download.pytorch.org/whl/torch_stable.html).

## Projects

This repository contains the following projects:

- `Project 1: 3D Space Agent`: An agent that moves in 3D space trying to hit a target.


## Usage
To train the agent using the provided configuration file, run:
```
ml-agents config/move3d.yaml --run-id=some_name
```
To initialize training from an existing brain, run:

```
ml-agents config/Training_config.yaml --initialize-from=BrainName --run-id=SomeName
```
Replace `BrainName` with the name of the brain to use as a starting point and `SomeName` with your desired run ID. Check results/run-id for the model checkpoints and training stats. Experiment with different hyperparameters and settings in the config file. Have fun exploring!

## ML-Agents

[ML-Agents](https://github.com/Unity-Technologies/ml-agents) is an open-source toolkit by Unity Technologies for creating intelligent agents that can learn and interact with their environments. It supports both reinforcement learning and imitation learning, and allows for a combination of both to take advantage of their respective strengths.

## The Cycle of Observation, Decision, Action, and Reward

The RL training process for an agent can be broken down into the following cycle:

1. **Observation**: The agent observes the current state of the environment through its sensors, which are typically represented as vectors or tensors.
2. **Decision**: The agent's neural network processes the observations and generates a set of actions that it thinks will maximize the expected future reward.
3. **Action**: The agent executes the actions generated by the neural network in the environment.
4. **Reward**: The agent receives a scalar reward signal from the environment that reflects the quality of its actions during the current time step.

This cycle repeats for each time step in the training episode. The agent's goal is to learn a policy that maximizes the cumulative reward over the entire episode. The training process typically involves adjusting the agent's neural network weights based on the observed rewards, so that the agent's behavior gradually improves over time.```

### Reinforcement Learning

- An agent learns to make decisions in an environment by interacting with it and receiving rewards or penalties for its actions.
- The goal is to maximize the cumulative reward over time.

- `OnEpisodeBegin()`: This function is called at the beginning of each episode (i.e., each time the agent starts a new task or resets its environment) and useful to setup the training enviornment.
- `CollectObservations(VectorSensor sensor)`: This function is called each time the agent needs to provide observations of the environment to its neural network.
- `OnActionReceived(ActionBuffers actions)`: This function is called each time the agent takes an action in the environment.
- `Heuristic(in ActionBuffers actionsOut)`: This function is used to provide a simple "baseline" policy for the agent to follow during testing or when a human player is controlling the agent.
- `SetReward(float value)`: This function is used to set the reward signal for the current time step.

### Imitation Learning

- An agent learns from a set of expert demonstrations rather than trial-and-error interactions with the environment.
- The expert demonstrations show the agent how to perform the task correctly.
- The agent learns to mimic the expert's behavior by learning a mapping from states to actions.

### Proximal Policy Optimization (PPO)

- PPO is a policy gradient method for reinforcement learning.
- It updates the policy by taking multiple steps per update, and constraining the policy update to be within a "trust region" around the current policy.
- This leads to more stable training and better sample efficiency compared to other policy gradient methods.
- ML-Agents includes PPO as one of its built-in reinforcement learning algorithms.

### Combination of Reinforcement and Imitation Learning

- ML-Agents allows for a combination of reinforcement and imitation learning to take advantage of their respective strengths.
- This approach is useful when the reward signal is sparse or difficult to define, or when the agent needs to learn from a mixture of good and bad examples.
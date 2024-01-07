# Celeste 2D Game Controller Case Study

<img src="/Documentation/0.jpg" width="260">

## Overview

Welcome to the Celeste 2D Game Controller Case Study! This project delves into the intricate design and implementation of the 2D game controller used in the critically acclaimed game, Celeste. Gain insights into the underlying platformer mechanics and intricacies that make Celeste's gameplay smooth and engaging.

## Features

These objectives showcase the attention to detail and advanced mechanics that contribute to the nuanced and satisfying gameplay experience in Celeste. The primary objectives of this case study include:

- Analyzing the 2D game controller system in Celeste.
- Documenting the control schemes for different platforms.
- Examining special effects such as Ghost Trail, Particles, and Screen Ripple.
- **Jump Buffering and Coyote Time:** Allows short-time jumping after leaving a ledge and executing a precise jump on the exact landing frame by holding the jump button just before landing.
- **Jump Corner Correction:** Smooths player movement by attempting to wiggle around corners when hitting the head.
- **Dash Corner Correction:** Pops the player onto ledges when dashing sideways and clipping a corner, ensuring seamless navigation.
- **One-way Platform Interaction:** Enhances traversal by popping the player onto semi-solid platforms when dashing sideways through them.

## Screenshots

<img src="/Documentation/1.png" width="560">
<img src="/Documentation/2.png" width="560">
<img src="/Documentation/3.png" width="560">

## Requirements

- Unity 2021.3.9f1 and up

## Getting Started

1. Clone the repository:

    ```bash
    git clone https://github.com/ProdiG66/2D-Game-Controller.git
    ```

2. Open the project in Unity.

3. Play using _Scenes/Play.scene

## Packages Utilized

The Celeste 2D Game Controller makes use of several powerful Unity packages to achieve its gameplay finesse:

- **URP (Universal Render Pipeline):** Enhances visual fidelity and performance.
- **Cinemachine:** Facilitates dynamic camera control for seamless player experience.
- **TextMeshPro:** Elevates text rendering quality for a polished UI.
- **Input System:** Enables efficient and flexible handling of player input.
- **DoTween Free:** Implements smooth tweens and animations.
- **Naughty Attributes:** Simplifies editor scripting for a more streamlined development process.

## Controls

Understanding the controls is crucial for mastering Celeste's challenging gameplay. The following table provides a comprehensive overview for different platforms:

| Controls |    PC     | Playstation |    Xbox    |   Switch   |
|:--------:|:---------:|:-----------:|:----------:|:----------:|
|   Move   |   WASD    | Left Stick  | Left Stick | Left Stick |
|   Jump   |   Space   |  Cross (X)  |     A      |     B      |
|   Dash   | Shift/LMB | Square (▢)  |     X      |     Y      |
|  Attack  |     F     | Circle (◯)  |     B      |     A      |

## Effects

Explore the magic behind Celeste's immersive gameplay with the following effects:

- **Ghost Trail**
- **Particles**
- **Screen Ripple**

## Contributing

If you'd like to contribute to this project, feel free to fork the repository and submit a pull request. Bug reports, suggestions, and feedback are also welcome!

## License

This project is licensed under the [MIT License](LICENSE).

Enjoy playtesting this case study!

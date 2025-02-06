# 60 Second Slam

A hypercasual basketball game developed with Unity where players aim to score baskets by bouncing the ball off a moving platform into moving hoops.

## 🎮 Game Description

60 Second Slam is an engaging hypercasual game that combines elements of classic breakout games with basketball. Players control a moving platform to bounce a basketball into moving hoops to score points. The game features:

- Simple and addictive gameplay mechanics
- Moving platform controlled by arrow keys
- Dynamic basketball hoops that move up and down
- Power-up system to enhance gameplay
- Mission-based progression system
- Multiple levels with increasing difficulty

## 🕹️ How to Play

1. Use the **Left Arrow** and **Right Arrow** keys to move the platform
2. Bounce the basketball off the platform to aim it into the hoops
3. Score the required number of baskets to complete each level
4. Collect power-ups to make the hoop wider for easier scoring
5. Avoid letting the ball fall below the platform

## 🛠️ Technical Details

### Core Components

- **Ball System**: Handles ball physics and collision detection with hoops and game over zones
- **Platform Controller**: Manages player input and platform movement within boundaries
- **Game Manager**: Controls game state, score tracking, and level progression
- **Power-up System**: Implements temporary gameplay modifications like wider hoops

### Key Features

- Smooth platform movement using Vector3.Lerp
- Mission tracking system with visual feedback
- Power-up system with duration-based effects
- Dynamic UI elements showing progress and score
- Collision-based scoring system

### Assets Structure

```
Assets/
├── Animations/        # Hoop movement animations
├── Envar Objects/    # Game objects and UI elements
├── Materials/        # Physics and visual materials
├── Models/          # 3D models for game objects
├── Prefabs/         # Reusable game components
└── Scripts/         # Game logic and controls
```

## 🎨 Art Style

The game features a clean and modern visual style with:
- Colorful basketball and platform designs
- Animated hoops with smooth movements
- Clear visual feedback for scoring and power-ups
- Minimalist UI design for better player focus

## 🔧 Development

The game is developed using:
- Unity Game Engine
- C# for game logic
- Unity's built-in physics system
- Custom animations for object movement

## 🎯 Future Improvements

Potential features for future updates:
- Additional power-up types
- More challenging levels
- Different ball types with unique properties
- Global leaderboard system
- Achievement system

## 📝 Credits

Developed by Bora Cingöz

---

Feel free to contribute to this project by submitting issues or pull requests!

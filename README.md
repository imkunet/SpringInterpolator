# SpringInterpolator Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver) [![](https://img.shields.io/github/downloads/Kuuuube/SpringInterpolator/total.svg)](https://github.com/Kuuuube/SpringInterpolator/releases/latest)

Interpolates your pointer based on a spring physics system.

## Explanation of the Values:

<br>

**Acceleration:** Controls the acceleration of the motion of the spring towards the target (Higher -> Faster) (Default 0.9).

**Dampening:** Dampening controls what the motion of the spring is multiplied by each physics tick (Higher -> Less dampening) (Default: 0.3).

**Speed:** This value controls the speed at which the spring simulation runs (Higher -> More "sluggish") (Default: 40.0)

<br>

## Example Settings:
<p align="middle">
  <img src="https://raw.githubusercontent.com/imkunet/SpringInterpolator/master/example_settings.png" align="middle"/>
</p>

These settings accelerate at 0.9/tick, dampen the value x0.3/tick, and run at 1 tick / 40ms

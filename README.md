# SpringInterpolator

[![](https://img.shields.io/github/downloads/imkunet/SpringInterpolator/total.svg)](https://github.com/imkunet/SpringInterpolator/releases/latest)

*this plugin is intended for use with the [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver) software*


### How it works
The key to smooth natural and dynamic animation is to act in a way that the user expects. Using a spring like physics
system is a great way to make the user be able to predict as to where the cursor will be in tablet applications. The
application of the *damped harmonic oscillator* which for brevity's sake will be dubbed **spring** in this project is
responsible for the interpolation in this plugin.

### Explanation of the Values:

**Stiffness:** Controls how stiff the feeling is for the cursor to catch up to the pen (Lower -> Stiffer) (Default 1.5)

**Damping:** Damping controls how much "friction" there is (Higher -> More friction) (Default: 3.0)

**StepSize:** Controls how fast the spring simulation is run (Higher -> Slower) (Default: 40.0)

## Example Settings:
<p align="middle">
  <img src="https://raw.githubusercontent.com/imkunet/SpringInterpolator/master/example_settings.png" align="middle" alt="settings example"/>
</p>

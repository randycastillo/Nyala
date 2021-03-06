# Nyala
Code Kata Challenge

## Requirements
Mars rover kata
 - You are given the initial starting point (x,y) of a rover and the direction (N,S,E,W) it is facing.
 - The rover receives a character array of commands.
 - Implement commands that move the rover forward/backward (f,b).
 - Implement commands that turn the rover left/right (l,r).
 - Implement wrapping at edges. But be careful, planets are spheres. Connect the x edge to the other x edge, so (1,1) for x-1 to (5,1), but connect vertical edges towards themselves in inverted coordinates, so (1,1) for y-1 connects to (5,1).
 - Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point, aborts the sequence and reports the obstacle.

## Overview
- Implementation:
  - Plain codes(kata style), no architectural design and patterns
  - Showcase TDD and a just a glimpse of BDD tool using specflow

## Project structure
- src
  - Nyala.Core
- tests
  - Nyala.Core.Acceptance.Tests
  - Nyala.Core.Unit.Tests

## Technology stack
- C#
- Net 6
- XUnit
- NSubstitute
- FluentAssertions
- Specflow

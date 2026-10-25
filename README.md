# Richard Carp -- TDL Programming Exercise

## Problem summary

Build a program that simulates robots navigating a bounded rectangular grid on Mars and reports each robot's final position and orientation.

The grid's lower-left corner is `(0, 0)`, and the input specifies its inclusive upper-right coordinates. Each robot starts at a coordinate facing north, east, south, or west and follows a sequence of instructions:

- `L`: turn left 90 degrees without moving.
- `R`: turn right 90 degrees without moving.
- `F`: move forward one grid point in the current direction. North increases the y-coordinate.

If a robot moves beyond the grid, it is lost and stops executing instructions. It leaves a scent at its last valid coordinate. Later robots ignore any instruction that would take them off the grid from a scented coordinate and continue with their remaining instructions.

Robots are processed sequentially, sharing scents across the simulation. For each robot, the program outputs its final coordinates and orientation, followed by `LOST` if it fell off the grid. A lost robot's reported position is its last valid position.

### Input and constraints

- The first line contains the grid's upper-right coordinates. (e.g. `5 3`)
- Each robot is described by two lines:
  - its starting coordinates and orientation (e.g. `1 1 E`)
  - then its instruction string. (e.g. `RFRFRFRF`)
- Coordinates cannot exceed 50, and instruction strings contain fewer than 100 characters.
- The design should allow additional command types in the future.

### Assumptions and design decisions

1. **Invalid input:** As the brief does not define this behaviour, any input outside the defined constraints immediately stops processing and raises an exception.
2. **Validation before simulation:** Validate all input before simulation begins to prevent partial results being output for invalid input.

### Scope

The submission is a runnable program with unit tests and documented setup and technical choices. A user interface, web application, database, and deployment pipeline are outside the scope of the exercise.

## Delivery plan

Start with a runnable journey using hard-coded structured data and expand its behaviour in tested slices. Slices 1-4 require no input parsing; text input is introduced in slice 5, followed by complete validation coverage.

| Slice | Functionality delivered | Example acceptance check |
|---|---|---|
| **1. One safe journey** | Use hard-coded grid bounds, one robot, and forward commands; move north within bounds and print its final position, with no input parsing | A hard-coded `5 3` grid and robot at `1 1 N` executing two forward commands produces `1 3 N` |
| **2. Full navigation** | Support all starting orientations and `L`, `R`, `F` | The sample's first robot produces `1 1 E` |
| **3. Boundary loss** | Detect leaving the grid, stop that robot, and output its last valid position with `LOST` | The sample's second robot produces `3 3 N LOST` |
| **4. Multiple robots and scents** | Execute a hard-coded collection of robots sequentially and share scents | The complete sample, represented as structured data, produces all three expected output lines |
| **5. Input parsing** | Parse grid bounds, robot positions, and instruction strings from text and connect them to the simulation | The supplied sample text produces all three expected output lines end to end |
| **6. Complete validation** | Finish constraint and malformed-input coverage; reject invalid input before simulation | An invalid later robot causes an exception with no simulation output |

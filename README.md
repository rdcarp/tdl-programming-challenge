# Richard Carp -- TDL Programming Exercise

## Problem summary

Build a program that simulates robots navigating a bounded rectangular grid on Mars and reports each robot's final position and orientation.

The grid's lower-left corner is `(0, 0)`, and the input specifies its inclusive upper-right coordinates. Each robot starts at a coordinate facing north, east, south, or west and follows a sequence of instructions:

- `L`: turn left 90 degrees without moving.
- `R`: turn right 90 degrees without moving.
- `F`: move forward one grid point in the current direction. North increases the y-coordinate.

If a robot moves beyond the grid, it is lost and stops executing instructions. It leaves a scent at its last valid position and orientation, marking the edge it crossed. Later robots ignore any instruction that would take them off the grid from that same position and orientation and continue with their remaining instructions.

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
3. **C# and .NET:** Use my most experienced stack so the limited exercise time can be spent on solving the problem and demonstrating the approach.
4. **Domain-driven design:** Keep domain concepts, business rules, and simulation orchestration in an application class library, separate from the CLI. Use domain language and encapsulate rules in the model, keeping the design proportionate to the exercise.
5. **CLI boundary:** The CLI references the application project and handles console input, output formatting, and error rendering. The application must not depend on the CLI or console APIs.
6. **Test-driven development:** Develop application functionality through red-green-refactor: write a failing behaviour test, implement enough to pass, then refactor with tests passing.
7. **Testing tools:** Use xUnit, Shouldly assertions, and NSubstitute for test doubles where dependencies need substitution. These are familiar tools that minimise overhead within the exercise's time limit.
8. **Test structure:** Use `Given_A_{SUT}/When_{Specific_Action}.cs` with a matching class name and `Then_{Expected_Assertion}` methods, capitalising each word and separating words with underscores. Use Arrange / Act / Assert; shared setup may live in the constructor with an `_sut` field, while each test performs its own action and checks one focused outcome with minimal assertions.
9. **Domain model:** In robotics, a position and orientation together form a **pose**. `Position` and `Pose` are immutable value objects. A `Robot` is constructed with its starting pose; `Simulate(commands, grid)` returns an immutable `SimulationReport` with its `EndPose`. Simulation state is local to each call; repeating a simulation starts from the original pose. The grid supplies inclusive bounds and shared scents.

10. **Globalisation:** Produced messages, including errors, are not globalised or localised; they are written in English for this exercise.
11. **Future instructions:** Use a `RobotCommand` enum and a switch in `Robot.Simulate`. Add command behaviours as needed; strategies and factories are deferred until a concrete requirement justifies them.

12. **Boundary loss:** `Grid.Contains(position)` checks the inclusive rectangle from `(0, 0)` to its upper-right coordinate. `Robot.Simulate(commands, grid)` checks each resulting position before applying it. Leaving the grid from an unscented pose immediately returns a report with `IsLost = true` and the last valid pose; the CLI appends `LOST`. Grid bounds and starting poses are assumed valid until the validation slice.

13. **Shared scents:** An immutable `GridTileEdge` value object identifies a boundary crossing by position and orientation. The grid stores scented edges in a `HashSet<GridTileEdge>`; `Pose` remains a description of a robot's positioning. A scent prevents only an off-grid move across that same edge. At a corner, a scent facing north does not protect an eastward departure. Turns and safe moves still execute, including northward movement alongside a scented east edge. Reusing a grid preserves its scents, while using a new grid starts without scents.
14. **Sequential simulation:** `RobotCommandList` pairs a starting pose with structured commands. `Simulation.Run` creates a fresh grid for each collection, executes command lists in order using that shared grid, and returns an ordered read-only collection of reports. The CLI supplies the hard-coded command lists and formats the reports.

### Scope

The submission is a runnable program with unit tests and documented setup and technical choices. A user interface, web application, database, and deployment pipeline are outside the scope of the exercise.

## Development setup

Install the .NET 10 SDK and open `src/TDL.MartianRobots.slnx` in your IDE.

From the repository root, build the solution with:

```shell
dotnet build src/TDL.MartianRobots.slnx
```

The solution contains the application class library, a separate CLI, and separate application and CLI test projects using xUnit and Shouldly. NSubstitute is available for future dependency doubles; the current domain tests use real objects.

Run the tests and the hard-coded command lists from the repository root:

```shell
dotnet test src/TDL.MartianRobots.slnx
dotnet run --project src/TDL.MartianRobots.Cli
```

Slice 4 supports full navigation, boundary loss, and multiple robots with shared scents using hard-coded structured data. The CLI runs all three sample command lists sequentially on a `5 3` grid. The second robot leaves a scent that protects the third robot, producing:

```text
1 1 E
3 3 N LOST
2 3 S
```

Models assume valid data and perform no runtime validation. Text parsing and validation remain for later slices.

### AI usage

AI assisted with planning, documentation, project setup, and the first two slices' implementation and tests. The developer selected the technology, architecture, and delivery order. Slice 2 verification included observing failing direction, turn, and sample command tests before implementation, then passing all 35 tests and checking the CLI output `1 1 E`. Human review and merging remain required.

For slice 4, AI assisted with shared scents, sequential simulation, tests, and documentation. Verification included observing failing scent and orchestration tests before implementation, plus a failing regression test for the clarified directional scent requirement. The updated implementation builds with no warnings, passes all 62 tests, and produces the three expected CLI output lines.

## Delivery plan

Development and testing conventions are defined in [AGENTS.md](AGENTS.md).

Start with a runnable command list using hard-coded structured data and expand its behaviour in tested slices. Slices 1-4 require no input parsing; text input is introduced in slice 5, followed by complete validation coverage.

| Slice | Functionality delivered | Example acceptance check |
|---|---|---|
| **1. One safe command list** | Use hard-coded grid bounds, one robot, and forward commands; move north within bounds and print its final position, with no input parsing | A hard-coded `5 3` grid and robot at `1 1 N` executing two forward commands produces `1 3 N` |
| **2. Full navigation** | Support all starting orientations and `L`, `R`, `F` | The sample's first robot produces `1 1 E` |
| **3. Boundary loss** | Detect leaving the grid, stop that robot, and output its last valid position with `LOST` | The sample's second robot produces `3 3 N LOST` |
| **4. Multiple robots and scents** | Execute a hard-coded collection of robots sequentially and share scents | The complete sample, represented as structured data, produces all three expected output lines |
| **5. Input parsing** | Parse grid bounds, robot positions, and instruction strings from text and connect them to the simulation | The supplied sample text produces all three expected output lines end to end |
| **6. Complete validation** | Finish constraint and malformed-input coverage; reject invalid input before simulation | An invalid later robot causes an exception with no simulation output |

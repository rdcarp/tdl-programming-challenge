# Richard Carp -- TDL Programming Exercise

## Problem summary

Simulate robots on a bounded Martian grid, reporting their final positions and whether they are lost. See the [programming challenge brief](docs/TDL_Developer_Programming_Challenge.pdf) for the rules, constraints, and sample input/output.

## Development setup and running

Install the .NET 10 SDK. From the repository root:

```shell
dotnet build src/TDL.MartianRobots.slnx
dotnet test src/TDL.MartianRobots.slnx
dotnet run --project src/TDL.MartianRobots.Cli
```

The CLI currently runs the three sample robots using hard-coded command lists:

```text
1 1 E
3 3 N LOST
2 3 S
```

Text parsing and input validation are planned for [slices 5 and 6](#delivery-plan). Development and testing conventions are in [AGENTS.md](AGENTS.md).

## Assumptions and design decisions

1. **Technology:** C# and .NET, using my most experienced stack. Tests use xUnit and Shouldly, with NSubstitute available for dependency doubles.
2. **Application boundary:** Domain rules and simulation orchestration belong in the application library. The separate CLI owns input, output formatting, errors, and exit behaviour.
3. **Invalid input:** Validate all input before simulation, raising an exception for invalid data without producing partial output.
4. **Scents:** A `GridTileEdge` identifies a boundary crossing by position and orientation. Scents prevent crossing that same edge; they do not block safe movement or another edge at a corner.
5. **Extensibility:** Keep command dispatch in `Robot.Simulate` using `RobotCommand`. Add abstractions when behaviour requires them.

## Delivery plan

Track progress on the [GitHub issues page](https://github.com/rdcarp/tdl-programming-challenge/issues).

| Slice | Functionality delivered | Example acceptance check |
|---|---|---|
| **1. One safe command list** | Use hard-coded grid bounds, one robot, and forward commands; move north within bounds and print its final position, with no input parsing | A hard-coded `5 3` grid and robot at `1 1 N` executing two forward commands produces `1 3 N` |
| **2. Full navigation** | Support all starting orientations and `L`, `R`, `F` | The sample's first robot produces `1 1 E` |
| **3. Boundary loss** | Detect leaving the grid, stop that robot, and output its last valid position with `LOST` | The sample's second robot produces `3 3 N LOST` |
| **4. Multiple robots and scents** | Execute a hard-coded collection of robots sequentially and share scents | The complete sample, represented as structured data, produces all three expected output lines |
| **5. Input parsing** | Parse grid bounds, robot positions, and instruction strings from text and connect them to the simulation | The supplied sample text produces all three expected output lines end to end |
| **6. Complete validation** | Finish constraint and malformed-input coverage; reject invalid input before simulation | An invalid later robot causes an exception with no simulation output |

## Next steps

If I were to progress this challenge further I would...

1. Complete slices 5 & 6 to complete the full input -> output flow, including validation of the specifications constraints.
2. Introduce structured diagnostic logging, tracking key events -- starting a simulation, encountering a scent, becoming lost, etc
3. Evolve instruction handling when new commands require it -- introduction of more command types with make for an unmanageable `switch` statement. The introduction of command handlers or strategies could be considered when further requirements were understood.
4. Introduce CI building and testing that produces an executable output

## AI usage

AI (codex) assisted with planning, issue tracking (on GitHub), documentation, project setup and skeleton implementations of issues. All changes are reviewed by human before merging. The developer selected the technology, architecture, and delivery order.

# Development conventions

## Scope and delivery

- Follow the requirements, assumptions, and delivery order in README.md.
- Work on the requested slice and keep changes small and cohesive.
- Use hard-coded structured data for slices 1-4; introduce text parsing in slice 5.
- Keep slice 1 focused on the valid hard-coded journey: do not add runtime model validation or temporary unsupported-behaviour guards. Introduce validation and boundary-loss behaviour in their planned slices.
- Keep the implementation proportionate to this short programming exercise.
- Record new assumptions and consequential design decisions in the README's numbered list.

## Branches and pull requests

- Perform all agent work on a dedicated branch created from `main`; never work or commit directly on `main`.
- Continue related work on its existing branch when appropriate.
- Submit changes through a pull request for human review. Agents must never merge or complete pull requests, enable auto-merge, or bypass the review process.
- A human is the final approval and merge gateway for all changes.

## Technology and architecture

- Use C# and .NET.
- Prefix the solution and all project names with `TDL.`.
- Keep domain concepts, business rules, and simulation orchestration in an application class library. Apply domain-driven design through consistent domain language and encapsulated business rules.
- Keep the CLI in a separate project referencing the application project. The application must not reference the CLI or use console APIs.
- The CLI owns console input, output formatting, error rendering, and process exit behaviour. Keep simulation rules out of it.
- Return structured results from the application. Represent invalid input with relevant exceptions; the CLI renders these as errors. A lost robot is a normal simulation result, not an exception.
- Introduce abstractions when required by behaviour or a real dependency boundary. Do not add repositories, domain events, or extra projects solely to demonstrate DDD.
- Keep journey execution and command dispatch in `Robot.Simulate(commands)`. Construct the robot with its starting `Pose` (position and orientation), keep journey state local to each simulation, and return an immutable `SimulationReport` with its `EndPose`. Introduce a world grid when bounds are needed in slice 3 and shared scents in slice 4. Defer strategies, factories, and movement proposal abstractions until needed.

## Test-driven development

- Develop application functionality with red-green-refactor: write a focused test, run it and confirm it fails for the expected reason, implement the minimum behaviour to pass, then refactor while keeping tests passing.
- Use xUnit as the test framework and Shouldly for assertions.
- Use Arrange / Act / Assert within tests. Shared arrangement and SUT creation may live in the test class constructor, using a private readonly `_sut` field and underscore-prefixed private instance fields. Keep the action under test in each method. Include `// Arrange` when the method needs additional setup, followed by `// Act` and `// Assert`.
- Organise tests in `Given_A_{SUT}/When_{Specific_Action}.cs`, where SUT means system under test. Capitalise each word in scenario and assertion names and separate words with underscores.
- Name the test class exactly as its file, excluding `.cs`.
- Name test methods `Then_{Expected_Assertion}`, keeping each method focused on its expected outcome.
- For example: `Given_A_Robot/When_A_North_Facing_Bot_Simulates_Moving_Forward.cs`, containing class `When_A_North_Facing_Bot_Simulates_Moving_Forward` and methods such as `Then_Position_Is_Updated` and `Then_Orientation_Remains_Unchanged`.
- Test one behaviour per test. Assert as little as necessary to prove that behaviour; avoid unrelated assertions and assertions about implementation details.
- Use NSubstitute when a dependency needs a test double. Use real domain objects for domain behaviour tests, and do not invent dependencies just to substitute them.
- Add regression tests when fixing behavioural defects.

## Coding style

- Use clear domain names and small, cohesive methods.
- Prefer primary constructors where possible, when they keep initialisation clear and straightforward.
- Use PascalCase for types and public members, and camelCase for parameters and local variables.
- Keep state changes explicit and enforce domain invariants within the application.
- Nullable checking is enabled centrally in `Directory.Build.props`, with nullable warnings treated as build errors. Use `?` for types that intentionally allow null and handle nullable values before dereferencing them. Fix nullable diagnostics rather than suppressing them or using the null-forgiving operator to bypass checking.
- Prefer straightforward code and only add dependencies that serve a concrete requirement.
- Keep movement and turning rules in `Robot`; `Pose` is an immutable description of position and orientation. Test these rules through `Robot.Simulate`.

## Verification

- From the repository root, build with `dotnet build src/TDL.MartianRobots.slnx` and, once test projects exist, run tests with `dotnet test src/TDL.MartianRobots.slnx`.
- Confirm the affected tests and the full test suite pass before completing an implementation change.
- Report checks performed and any checks that could not run. Do not claim verification that was not performed.
- Update setup and execution instructions when tooling or application entry points are introduced.

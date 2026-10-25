# Development conventions

## Scope and delivery

- Follow the requirements, assumptions, and delivery order in README.md.
- Work on the requested slice and keep changes small and cohesive.
- Use hard-coded structured data for slices 1-4; introduce text parsing in slice 5.
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

## Test-driven development

- Develop application functionality with red-green-refactor: write a focused test, run it and confirm it fails for the expected reason, implement the minimum behaviour to pass, then refactor while keeping tests passing.
- Use xUnit as the test framework and Shouldly for assertions.
- Structure each test with explicit `// Given`, `// When`, and `// Then` sections for setup, the action, and assertions.
- Give tests descriptive names that communicate the scenario and expected outcome.
- Test one behaviour per test. Assert as little as necessary to prove that behaviour; avoid unrelated assertions and assertions about implementation details.
- Use NSubstitute when a dependency needs a test double. Use real domain objects for domain behaviour tests, and do not invent dependencies just to substitute them.
- Add regression tests when fixing behavioural defects.

## Coding style

- Use clear domain names and small, cohesive methods.
- Use PascalCase for types and public members, and camelCase for parameters and local variables.
- Keep state changes explicit and enforce domain invariants within the application.
- Prefer straightforward code and only add dependencies that serve a concrete requirement.

## Verification

- From the repository root, build with `dotnet build src/TDL.MartianRobots.slnx` and, once test projects exist, run tests with `dotnet test src/TDL.MartianRobots.slnx`.
- Confirm the affected tests and the full test suite pass before completing an implementation change.
- Report checks performed and any checks that could not run. Do not claim verification that was not performed.
- Update setup and execution instructions when tooling or application entry points are introduced.

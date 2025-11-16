---

description: "Task list template for feature implementation"
---

# Tasks: [FEATURE NAME]

**Input**: Design documents from `/specs/[###-feature-name]/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: Per Smart Flow UI Constitution Principle II, tests are MANDATORY, not optional. Test coverage of 80% for business logic, integration tests for all API endpoints, and component tests for complex Blazor interactions are required.

**Test-First Development**: Tests MUST be written FIRST, verified to FAIL, then implementation begins (Red-Green-Refactor cycle).

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Path Conventions

- **Single project**: `src/`, `tests/` at repository root
- **Web app**: `backend/src/`, `frontend/src/`
- **Mobile**: `api/src/`, `ios/src/` or `android/src/`
- Paths shown below assume single project - adjust based on plan.md structure

<!-- 
  ============================================================================
  IMPORTANT: The tasks below are SAMPLE TASKS for illustration purposes only.
  
  The /speckit.tasks command MUST replace these with actual tasks based on:
  - User stories from spec.md (with their priorities P1, P2, P3...)
  - Feature requirements from plan.md
  - Entities from data-model.md
  - Endpoints from contracts/
  
  Tasks MUST be organized by user story so each story can be:
  - Implemented independently
  - Tested independently
  - Delivered as an MVP increment
  
  DO NOT keep these sample tasks in the generated tasks.md file.
  ============================================================================
-->

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [ ] T001 Create project structure per implementation plan
- [ ] T002 Initialize [language] project with [framework] dependencies
- [ ] T003 [P] Configure linting and formatting tools (.editorconfig, analyzers)
- [ ] T004 [P] Setup test projects mirroring production structure (*.Tests)
- [ ] T005 [P] Configure code analysis (enable CA rules, treat warnings as errors in Release)
- [ ] T006 [P] Setup CI/CD pipeline with test runs and coverage reporting

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

Examples of foundational tasks (adjust based on your project):

- [ ] T007 Setup database schema and migrations framework
- [ ] T008 [P] Implement authentication/authorization framework (Azure AD/Entra ID with RBAC)
- [ ] T009 [P] Setup API routing and middleware structure
- [ ] T010 Create base models/entities that all stories depend on
- [ ] T011 Configure structured logging (JSON) with correlation IDs and Application Insights
- [ ] T012 Setup environment configuration management (Key Vault integration, Managed Identity)
- [ ] T013 [P] Implement health checks for external dependencies
- [ ] T014 [P] Configure MudBlazor theming and base layout components
- [ ] T015 [P] Setup error handling middleware and user-facing error components (Snackbar, dialogs)

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - [Title] (Priority: P1) 🎯 MVP

**Goal**: [Brief description of what this story delivers]

**Independent Test**: [How to verify this story works on its own]

### Tests for User Story 1 (MANDATORY per Constitution) 🔴 RED

> **CRITICAL: Write these tests FIRST, run them, ensure they FAIL before implementation begins**

- [ ] T016 [P] [US1] Unit tests for [Entity1] model in [Project].Tests/Models/[Entity1]Tests.cs
- [ ] T017 [P] [US1] Unit tests for [Entity2] model in [Project].Tests/Models/[Entity2]Tests.cs
- [ ] T018 [P] [US1] Unit tests for [Service] in [Project].Tests/Services/[Service]Tests.cs
- [ ] T019 [P] [US1] Integration test for [API endpoint] in [Project].Tests/Integration/[Controller]Tests.cs
- [ ] T020 [P] [US1] Component test for [Blazor component] (if UI) in [Project].Client.Tests/Components/[Component]Tests.cs
- [ ] T021 [US1] Verify all tests FAIL (no implementation exists yet) - commit failing tests

### Implementation for User Story 1 🟢 GREEN

> **Only begin implementation after tests are written and failing**

- [ ] T022 [P] [US1] Create [Entity1] model in [Project]/Models/[Entity1].cs (with XML docs)
- [ ] T023 [P] [US1] Create [Entity2] model in [Project]/Models/[Entity2].cs (with XML docs)
- [ ] T024 [US1] Implement [Service] in [Project]/Services/[Service].cs (depends on T022, T023, with DI)
- [ ] T025 [US1] Implement [API endpoint] in [Project]/Controllers/[Controller].cs
- [ ] T026 [US1] Implement [Blazor component] (if UI) in [Project].Client/Components/[Component].razor
- [ ] T027 [US1] Add request/response logging with correlation IDs
- [ ] T028 [US1] Add user-facing error handling (Snackbar/dialog)
- [ ] T029 [US1] Add loading states for async operations
- [ ] T030 [US1] Verify all tests PASS - commit working implementation

### Refactor for User Story 1 🔵 REFACTOR

> **Clean up implementation while keeping tests green**

- [ ] T031 [US1] Code review: verify SRP, DI, type safety, naming conventions
- [ ] T032 [US1] Verify test coverage ≥80% for business logic
- [ ] T033 [US1] Performance check: API response time meets targets (p95 ≤500ms simple, ≤2s complex)
- [ ] T034 [US1] Accessibility check: keyboard nav, screen reader, ARIA labels (if UI)
- [ ] T035 [US1] Responsive design check: test mobile/tablet/desktop viewports (if UI)

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - [Title] (Priority: P2)

**Goal**: [Brief description of what this story delivers]

**Independent Test**: [How to verify this story works on its own]

### Tests for User Story 2 (MANDATORY per Constitution) 🔴 RED

> **CRITICAL: Write these tests FIRST, run them, ensure they FAIL before implementation begins**

- [ ] T036 [P] [US2] Unit tests for [Entity] model in [Project].Tests/Models/[Entity]Tests.cs
- [ ] T037 [P] [US2] Unit tests for [Service] in [Project].Tests/Services/[Service]Tests.cs
- [ ] T038 [P] [US2] Integration test for [API endpoint] in [Project].Tests/Integration/[Controller]Tests.cs
- [ ] T039 [P] [US2] Component test for [Blazor component] (if UI) in [Project].Client.Tests/Components/[Component]Tests.cs
- [ ] T040 [US2] Verify all tests FAIL - commit failing tests

### Implementation for User Story 2 🟢 GREEN

- [ ] T041 [P] [US2] Create [Entity] model in [Project]/Models/[Entity].cs (with XML docs)
- [ ] T042 [US2] Implement [Service] in [Project]/Services/[Service].cs (with DI)
- [ ] T043 [US2] Implement [API endpoint] in [Project]/Controllers/[Controller].cs
- [ ] T044 [US2] Implement [Blazor component] (if UI) in [Project].Client/Components/[Component].razor
- [ ] T045 [US2] Integrate with User Story 1 components (if needed)
- [ ] T046 [US2] Add logging, error handling, loading states
- [ ] T047 [US2] Verify all tests PASS - commit working implementation

### Refactor for User Story 2 🔵 REFACTOR

- [ ] T048 [US2] Code review and constitution compliance check
- [ ] T049 [US2] Verify test coverage ≥80%
- [ ] T050 [US2] Performance and accessibility validation (if applicable)

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - [Title] (Priority: P3)

**Goal**: [Brief description of what this story delivers]

**Independent Test**: [How to verify this story works on its own]

### Tests for User Story 3 (MANDATORY per Constitution) 🔴 RED

- [ ] T051 [P] [US3] Unit tests for [Entity] model in [Project].Tests/Models/[Entity]Tests.cs
- [ ] T052 [P] [US3] Unit tests for [Service] in [Project].Tests/Services/[Service]Tests.cs
- [ ] T053 [P] [US3] Integration test for [API endpoint] in [Project].Tests/Integration/[Controller]Tests.cs
- [ ] T054 [P] [US3] Component test for [Blazor component] (if UI) in [Project].Client.Tests/Components/[Component]Tests.cs
- [ ] T055 [US3] Verify all tests FAIL - commit failing tests

### Implementation for User Story 3 🟢 GREEN

- [ ] T056 [P] [US3] Create [Entity] model in [Project]/Models/[Entity].cs (with XML docs)
- [ ] T057 [US3] Implement [Service] in [Project]/Services/[Service].cs (with DI)
- [ ] T058 [US3] Implement [API endpoint] in [Project]/Controllers/[Controller].cs
- [ ] T059 [US3] Implement [Blazor component] (if UI) in [Project].Client/Components/[Component].razor
- [ ] T060 [US3] Add logging, error handling, loading states
- [ ] T061 [US3] Verify all tests PASS - commit working implementation

### Refactor for User Story 3 🔵 REFACTOR

- [ ] T062 [US3] Code review and constitution compliance check
- [ ] T063 [US3] Verify test coverage ≥80%
- [ ] T064 [US3] Performance and accessibility validation (if applicable)

**Checkpoint**: All user stories should now be independently functional

---

[Add more user story phases as needed, following the same pattern]

---

## Phase N: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] TXXX [P] Documentation updates (README, API docs, XML comments review)
- [ ] TXXX Code cleanup and refactoring (verify SRP, DI patterns)
- [ ] TXXX Performance optimization across all stories (profiling, caching)
- [ ] TXXX [P] Security audit (dependency vulnerabilities, secrets management, RBAC enforcement)
- [ ] TXXX Accessibility audit (WCAG 2.1 Level AA compliance across all UI)
- [ ] TXXX Test coverage review (ensure ≥80% for all business logic)
- [ ] TXXX CI/CD pipeline verification (build warnings=errors, test gates, coverage reports)
- [ ] TXXX Run quickstart.md validation

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
  - User stories can then proceed in parallel (if staffed)
  - Or sequentially in priority order (P1 → P2 → P3)
- **Polish (Final Phase)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) - No dependencies on other stories
- **User Story 2 (P2)**: Can start after Foundational (Phase 2) - May integrate with US1 but should be independently testable
- **User Story 3 (P3)**: Can start after Foundational (Phase 2) - May integrate with US1/US2 but should be independently testable

### Within Each User Story (Red-Green-Refactor Cycle)

- **RED Phase**: Write all tests FIRST, verify they FAIL, commit failing tests
- **GREEN Phase**: Implement minimal code to make tests pass, verify SUCCESS, commit working code
- **REFACTOR Phase**: Clean up code (SRP, naming, patterns), verify tests still pass, check coverage/performance/accessibility

- Tests (if included) MUST be written and FAIL before implementation
- Models before services
- Services before endpoints
- Core implementation before integration
- Story complete before moving to next priority

### Parallel Opportunities

- All Setup tasks marked [P] can run in parallel
- All Foundational tasks marked [P] can run in parallel (within Phase 2)
- Once Foundational phase completes, all user stories can start in parallel (if team capacity allows)
- All tests for a user story marked [P] can run in parallel
- Models within a story marked [P] can run in parallel
- Different user stories can be worked on in parallel by different team members

---

## Parallel Example: User Story 1

```bash
# Launch all tests for User Story 1 together (if tests requested):
Task: "Contract test for [endpoint] in tests/contract/test_[name].py"
Task: "Integration test for [user journey] in tests/integration/test_[name].py"

# Launch all models for User Story 1 together:
Task: "Create [Entity1] model in src/models/[entity1].py"
Task: "Create [Entity2] model in src/models/[entity2].py"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL - blocks all stories)
3. Complete Phase 3: User Story 1
4. **STOP and VALIDATE**: Test User Story 1 independently
5. Deploy/demo if ready

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add User Story 1 → Test independently → Deploy/Demo (MVP!)
3. Add User Story 2 → Test independently → Deploy/Demo
4. Add User Story 3 → Test independently → Deploy/Demo
5. Each story adds value without breaking previous stories

### Parallel Team Strategy

With multiple developers:

1. Team completes Setup + Foundational together
2. Once Foundational is done:
   - Developer A: User Story 1
   - Developer B: User Story 2
   - Developer C: User Story 3
3. Stories complete and integrate independently

---

## Notes

- [P] tasks = different files, no dependencies
- [Story] label maps task to specific user story for traceability
- Each user story should be independently completable and testable
- Verify tests fail before implementing
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- Avoid: vague tasks, same file conflicts, cross-story dependencies that break independence

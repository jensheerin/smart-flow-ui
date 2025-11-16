# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

[Extract from feature spec: primary requirement + technical approach from research]

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: [e.g., Python 3.11, Swift 5.9, Rust 1.75 or NEEDS CLARIFICATION]  
**Primary Dependencies**: [e.g., FastAPI, UIKit, LLVM or NEEDS CLARIFICATION]  
**Storage**: [if applicable, e.g., PostgreSQL, CoreData, files or N/A]  
**Testing**: [e.g., pytest, XCTest, cargo test or NEEDS CLARIFICATION]  
**Target Platform**: [e.g., Linux server, iOS 15+, WASM or NEEDS CLARIFICATION]
**Project Type**: [single/web/mobile - determines source structure]  
**Performance Goals**: [domain-specific, e.g., 1000 req/s, 10k lines/sec, 60 fps or NEEDS CLARIFICATION]  
**Constraints**: [domain-specific, e.g., <200ms p95, <100MB memory, offline-capable or NEEDS CLARIFICATION]  
**Scale/Scope**: [domain-specific, e.g., 10k users, 1M LOC, 50 screens or NEEDS CLARIFICATION]

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Code Quality Requirements

- [ ] Single Responsibility: Does each component/service have one clear purpose?
- [ ] Dependency Injection: Are dependencies injected, not hard-coded?
- [ ] Type Safety: Are nullable reference types used correctly? No `dynamic`/`object` abuse?
- [ ] Documentation: Do all public APIs have XML documentation comments?
- [ ] Code Review: Is peer review process defined and mandatory?

### Testing Standards

- [ ] Test Projects: Are test projects created mirroring production structure (`*.Tests`)?
- [ ] Unit Test Coverage: Is 80% coverage target achievable for business logic?
- [ ] Integration Tests: Are all API endpoints covered by integration tests?
- [ ] Component Tests: Are Blazor components with complex interactions tested with bUnit?
- [ ] Test Quality: Are tests deterministic, fast (<100ms), isolated, and using AAA pattern?
- [ ] CI/CD Gates: Are test runs and coverage reports part of PR validation?

### User Experience Consistency

- [ ] Design System: Is MudBlazor being used consistently?
- [ ] Accessibility: Are WCAG 2.1 Level AA requirements considered (keyboard nav, screen readers)?
- [ ] Responsive Design: Are mobile (≥375px), tablet (≥768px), desktop (≥1024px) viewports tested?
- [ ] Error Handling: Are user-facing errors clear, actionable, non-technical?
- [ ] Loading States: Do all async operations display appropriate loading indicators?
- [ ] Validation: Is form validation implemented with FluentValidation where complex?

### Performance & Observability

- [ ] Performance Budgets: Are targets defined (API p95 ≤500ms simple, ≤2s complex; page load ≤3s)?
- [ ] Logging: Is structured logging (JSON) implemented with correlation IDs?
- [ ] Health Checks: Are health checks implemented for external dependencies?
- [ ] Resource Efficiency: Are pagination (max 100 items), streaming (large files) used?
- [ ] Monitoring: Are Application Insights and alerting configured?

### Security & Compliance

- [ ] Authentication: Are all API endpoints (except health checks) authenticated?
- [ ] Authorization: Is RBAC enforced at API layer, not just UI?
- [ ] Secrets Management: Are secrets stored in Azure Key Vault, not config files?
- [ ] Data Protection: Is PII encrypted? Is Managed Identity used where possible?
- [ ] Dependency Security: Are dependencies current (≤30 days behind) and vulnerability-free?

### Violations Requiring Justification

If any constitutional requirements cannot be met, document in Complexity Tracking section below.

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)
<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
# [REMOVE IF UNUSED] Option 1: Single project (DEFAULT)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |

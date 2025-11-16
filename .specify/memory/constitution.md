<!--
SYNC IMPACT REPORT:
Version Change: N/A → 1.0.0 (Initial Constitution)
Added Sections:
  - Core Principles (4 principles established)
  - Performance & Quality Standards
  - Security & Compliance
  - Governance
Modified Principles: N/A (initial creation)
Templates Requiring Updates:
  ✅ plan-template.md - Updated with constitution check gates
  ✅ spec-template.md - Aligned with UX and testing requirements
  ✅ tasks-template.md - Aligned with test-first and quality principles
Follow-up TODOs: None
-->

# Smart Flow UI Constitution

## Core Principles

### I. Code Quality First

All code MUST meet the following non-negotiable quality standards:

- **Readability**: Code is written once, read many times. Use clear naming, appropriate abstractions, and self-documenting patterns. Avoid clever shortcuts that obscure intent.
- **Maintainability**: Every component MUST have a single, well-defined responsibility (Single Responsibility Principle). Dependencies MUST be injected, not hard-coded.
- **Type Safety**: Leverage C#'s type system fully. Use nullable reference types consistently. Avoid `dynamic`, `object`, or stringly-typed patterns unless absolutely justified.
- **Consistency**: Follow .NET coding conventions and project-established patterns. Use EditorConfig to enforce formatting rules. All public APIs MUST have XML documentation comments.
- **Code Reviews**: All code changes MUST be peer-reviewed before merge. Reviewers verify adherence to these principles, not just functionality.

**Rationale**: High code quality reduces defects, accelerates feature velocity, and makes the codebase accessible to all team members. Technical debt accumulates faster than it can be repaid—prevention is mandatory.

### II. Test Coverage & Quality Standards

Testing is NOT optional—it is a core engineering practice:

- **Unit Test Coverage**: All business logic and service layer code MUST have unit test coverage of at least 80%. Use xUnit as the standard testing framework.
- **Integration Tests**: All API endpoints MUST have integration tests verifying contract compliance, error handling, and authentication/authorization flows.
- **Component Tests**: Blazor components with complex interactions MUST have bUnit component tests validating rendering logic and user interactions.
- **Test Quality**: Tests MUST be deterministic, fast (<100ms for unit tests), isolated, and self-documenting. Use Arrange-Act-Assert (AAA) pattern consistently.
- **Test Project Structure**: Test projects MUST mirror production structure: `SmartFlow.UI.API.Tests`, `SmartFlow.UI.Client.Tests`, `Shared.Tests`.
- **CI/CD Gates**: All tests MUST pass before merge. Code coverage reports MUST be generated on every PR. Decreasing coverage blocks merge.

**Rationale**: Tests document intended behavior, enable confident refactoring, catch regressions early, and serve as executable specifications. Untested code is legacy code from day one.

### III. User Experience Consistency

The UI MUST deliver a predictable, accessible, and performant experience:

- **Design System**: Use MudBlazor components consistently across all pages. Custom components MUST follow MudBlazor patterns and theming.
- **Accessibility**: All interactive elements MUST be keyboard-navigable and screen-reader compatible (WCAG 2.1 Level AA minimum). Use semantic HTML and ARIA attributes correctly.
- **Responsive Design**: UI MUST function on mobile (≥375px), tablet (≥768px), and desktop (≥1024px) viewports. Test all breakpoints.
- **Error Handling**: User-facing errors MUST be clear, actionable, and non-technical. Use MudBlazor Snackbar for transient feedback, dialogs for critical errors.
- **Loading States**: All async operations MUST display loading indicators (spinners, skeletons, progress bars). Users MUST never see a frozen UI.
- **Validation**: Form validation MUST occur on blur and submit, with immediate inline feedback. Use FluentValidation for complex validation logic.

**Rationale**: Users judge quality by the interface. Consistency reduces cognitive load, accessibility is a legal and ethical requirement, and clear feedback builds trust.

### IV. Performance & Observability

The application MUST be fast, reliable, and diagnosable:

- **Performance Budgets**:
  - Initial page load: ≤3 seconds (3G network simulation)
  - API response time: p95 ≤500ms for simple queries, ≤2s for complex operations
  - Blazor component render time: ≤100ms for interactive components
  - Bundle size: Client WASM payload ≤5MB compressed
- **Observability**:
  - All API endpoints MUST log request/response with correlation IDs
  - Use Application Insights for distributed tracing across API and agents
  - Structured logging (JSON) with meaningful context (user ID, tenant ID, operation)
  - Health checks MUST be implemented for all external dependencies (Azure OpenAI, Azure AI Search, Blob Storage, Cosmos DB)
- **Resource Efficiency**:
  - API memory usage MUST remain ≤512MB under normal load
  - Database/search queries MUST use pagination (max 100 items per page)
  - Large file operations MUST use streaming, not in-memory buffering
- **Monitoring & Alerts**:
  - Track key metrics: error rate, latency percentiles, dependency health
  - Alert on: error rate >1%, p95 latency >2s, dependency failures >5%

**Rationale**: Performance directly impacts user satisfaction and retention. Observability enables rapid diagnosis of production issues. Resource efficiency controls Azure costs.

## Performance & Quality Standards

### Build & CI/CD Requirements

- All builds MUST succeed without warnings (treat warnings as errors in Release configuration)
- NuGet package versions MUST be managed centrally via `Directory.Packages.props`
- Docker images MUST be scanned for vulnerabilities before deployment
- Deployment pipelines MUST include smoke tests validating core functionality

### Code Analysis

- Enable and enforce .NET analyzers (CA rules) with appropriate severity
- Use SonarQube/SonarCloud for continuous code quality monitoring
- Security analysis MUST run on every commit (detect secrets, vulnerable dependencies)
- Cyclomatic complexity MUST remain ≤15 per method; higher values require justification

### Documentation Standards

- README MUST document: setup instructions, architecture overview, deployment process
- All configuration settings MUST be documented in `appsettings.Template.json` with descriptions
- API endpoints MUST have Swagger/OpenAPI documentation with examples
- Breaking changes MUST be documented in CHANGELOG.md with migration guides

## Security & Compliance

### Authentication & Authorization

- All API endpoints MUST require authentication except public health checks
- Use Azure AD/Entra ID for user authentication (Microsoft.Agents.Authentication.Msal)
- Role-based access control (RBAC) MUST be enforced at the API layer, not just UI
- Secrets MUST be stored in Azure Key Vault, never in appsettings.json or environment variables

### Data Protection

- Personally Identifiable Information (PII) MUST be encrypted at rest and in transit
- Azure Storage encryption MUST be enabled for all storage accounts
- Connection strings MUST use Managed Identity where possible, fallback to Key Vault secrets
- Logging MUST NOT include PII, credentials, or sensitive business data

### Dependency Management

- Dependencies MUST be kept up-to-date (maximum 30 days behind latest stable)
- Transitive dependency vulnerabilities MUST be resolved within 7 days of disclosure
- All production dependencies MUST have active maintenance and security support

## Governance

### Constitution Authority

This constitution supersedes all prior development practices, coding guidelines, and team conventions. Where conflicts arise, this document takes precedence.

### Amendment Process

1. **Proposal**: Any team member may propose amendments via PR to `.specify/memory/constitution.md`
2. **Review**: Amendments require approval from at least two senior engineers
3. **Migration Plan**: Breaking changes MUST include a migration plan for existing code
4. **Versioning**:
   - **MAJOR** version: Removes/redefines core principles or adds non-negotiable constraints
   - **MINOR** version: Adds new principles, expands sections, or strengthens guidance
   - **PATCH** version: Clarifications, typo fixes, or non-semantic edits
5. **Communication**: All amendments MUST be announced to the team with rationale

### Compliance Review

- All PRs MUST include a constitution compliance checklist
- Quarterly reviews assess adherence and identify debt requiring remediation
- Violations require documented justification and approval from tech lead
- Repeat violations trigger architecture review and refactoring

### Living Document

This constitution evolves with the project. As patterns emerge or constraints change, the team MUST update this document to reflect current practices. Outdated rules undermine authority—keep it current.

**Version**: 1.0.0 | **Ratified**: 2025-11-16 | **Last Amended**: 2025-11-16

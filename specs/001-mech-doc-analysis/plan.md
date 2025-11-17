# Implementation Plan: Mechanical Document Analysis for Sales Engineers

**Branch**: `001-mech-doc-analysis` | **Date**: 2025-01-23 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-mech-doc-analysis/spec.md`

**Note**: This plan extends the existing SmartFlow UI Blazor WebAssembly application with mechanical document analysis capabilities.

## Summary

**Core Requirement**: Enable sales engineers to upload mechanical specification PDFs for AI-powered analysis, extracting structured data (schedules, calculations, properties) and interacting through a chat interface with suggested questions.

**User Value**: Dramatically reduces manual document review time, improves accuracy, enables instant access to structured data and calculations, and provides conversational interface for exploring analysis results.

**Technical Approach**: 
- Extend existing Blazor WASM client with new pages/components (`DocumentAnalysis.razor`, `AnalysisResults.razor`, `DocumentChat.razor`)
- Add API controllers in SmartFlow.UI.API (`DocumentAnalysisController`, `ChatController`) to proxy requests to Document Agent backend
- Use existing Azure Blob Storage for document uploads (new container: `mech-doc-analysis`)
- Use existing Cosmos DB for session/chat persistence (new collections: `AnalysisSessions`, `ChatMessages`)
- Integrate with Document Agent Service (external API) for orchestrated analysis
- Leverage existing infrastructure: `ApiClient`, `BlobServiceClient`, MudBlazor components, authentication

## Technical Context

**Language/Version**: C# 12 / .NET 8.0  
**Primary Dependencies**: Blazor WebAssembly 8.0.14, MudBlazor 8.5.1, Semantic Kernel 1.66.0, Azure.Storage.Blobs 12.24.0, Microsoft.Azure.Cosmos 3.54.0  
**Storage**: Azure Cosmos DB (sessions/chat history), Azure Blob Storage (document uploads)  
**Testing**: xUnit 2.5.2 (unit), bUnit 1.24.10 (components), Microsoft.NET.Test.Sdk 17.7.2 (integration)  
**Target Platform**: Web browsers via Blazor WebAssembly + ASP.NET Core 8.0 API (Azure Container Apps)
**Project Type**: Web (Blazor WASM client + Web API backend)  
**Performance Goals**: API p95 ≤500ms (simple queries), ≤2s (complex analysis), page load ≤2s, component render ≤100ms, 50 concurrent users  
**Constraints**: 80% unit test coverage, WCAG 2.1 Level AA accessibility, 50MB file upload limit, PDF-only initially  
**Scale/Scope**: 4 new Blazor pages, ~15 components, 3 API controllers, 10 data models, 10 Cosmos DB entities, 5 Azure Blob containers

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Code Quality Requirements

- [x] Single Responsibility: Each component has one purpose (DocumentUpload, AnalysisResults, DocumentChat, FeedbackPanel)
- [x] Dependency Injection: All services injected via DI container (DocumentAnalysisService, ChatService, FeedbackService)
- [x] Type Safety: Nullable reference types enabled (`<Nullable>enable</Nullable>` in .csproj), no `dynamic` abuse
- [x] Documentation: All public APIs will have XML comments (`<summary>`, `<param>`, `<returns>`)
- [x] Code Review: PR review mandatory (enforced by branch protection, peer review required before merge)

### Testing Standards

- [x] Test Projects: Will create `SmartFlow.UI.API.Tests` and `SmartFlow.UI.Client.Tests` following existing structure
- [x] Unit Test Coverage: 80% target achievable (services, controllers, business logic fully covered)
- [x] Integration Tests: All 3 API controllers (`DocumentAnalysisController`, `ChatController`, `FeedbackController`) will have integration tests
- [x] Component Tests: bUnit tests for `DocumentUpload.razor`, `AnalysisResults.razor`, `DocumentChat.razor` (complex interactions, file uploads)
- [x] Test Quality: AAA pattern, isolated (mocked dependencies), deterministic, fast (<100ms unit, <1s integration)
- [x] CI/CD Gates: Test runs and coverage checks in PR pipeline (existing azure-pipelines.yml extends to include new tests)

### User Experience Consistency

- [x] Design System: MudBlazor 8.5.1 components used exclusively (`MudFileUpload`, `MudTable`, `MudCard`, `MudDataGrid`, `MudTextField`)
- [x] Accessibility: WCAG 2.1 AA compliant (keyboard navigation, ARIA labels, screen reader support, focus management)
- [x] Responsive Design: Mobile-first (≥375px), tablet (≥768px), desktop (≥1024px) breakpoints tested
- [x] Error Handling: User-friendly error messages ("Document analysis failed. Please try again"), retry mechanisms, validation feedback
- [x] Loading States: `MudProgressCircular`, `MudProgressLinear`, skeleton loaders for async operations (upload, analysis, chat)
- [x] Validation: File type validation (PDF only), file size validation (50MB limit), required field validation

### Performance & Observability

- [x] Performance Budgets: API p95 ≤500ms (simple queries), ≤2s (analysis requests), page load ≤2s, component render ≤100ms
- [x] Logging: Structured JSON logging with correlation IDs (`ILogger<T>`, Application Insights integration)
- [x] Health Checks: Health check endpoints for Document Agent API, Blob Storage, Cosmos DB (`/health/ready`, `/health/live`)
- [x] Resource Efficiency: Pagination (max 100 documents per request), streaming for large file downloads, incremental loading
- [x] Monitoring: Application Insights telemetry (custom events, dependencies, exceptions), alerting on p95 > 2s or 5xx errors

### Security & Compliance

- [x] Authentication: All API endpoints authenticated (inherit from existing authentication middleware, except `/health/*`)
- [x] Authorization: RBAC enforced at API layer (`[Authorize]` attributes, policy-based authorization for document access)
- [x] Secrets Management: Document Agent API key in Azure Key Vault (no config file storage), Managed Identity for Azure services
- [x] Data Protection: PII not stored (documents only), encrypted at rest (Blob Storage, Cosmos DB encryption enabled)
- [x] Dependency Security: Dependencies ≤30 days behind (existing packages current: Semantic Kernel 1.66.0, MudBlazor 8.5.1)

### Violations Requiring Justification

**No constitutional violations identified.** All requirements can be met within existing infrastructure and technology stack.

## Project Structure

### Documentation (this feature)

```text
specs/001-mech-doc-analysis/
├── spec.md              # Feature specification (completed)
├── plan.md              # This file (implementation plan)
├── research.md          # Phase 0: Technical feasibility, Document Agent API contracts (pending)
├── data-model.md        # Phase 1: 10 entities, relationships, validation rules (pending)
├── quickstart.md        # Phase 1: Developer onboarding, local setup, testing guide (pending)
├── contracts/           # Phase 1: Document Agent API OpenAPI spec, request/response schemas (pending)
│   ├── document-agent-api.yaml
│   ├── analysis-request.json
│   └── analysis-response.json
├── tasks.md             # Phase 2: Task breakdown for implementation (pending)
└── checklists/          # Quality validation checklists
    └── requirements.md  # Requirements checklist (completed)
```

### Source Code (repository root)

```text
# Web application: Blazor WASM frontend + ASP.NET Core backend

# Backend (API)
app/SmartFlow.UI.API/
├── Controllers/
│   ├── DocumentAnalysisController.cs       # NEW: Upload, analysis, session management
│   ├── DocumentChatController.cs           # NEW: Chat with analysis, suggested questions
│   └── FeedbackController.cs               # NEW: Feedback collection
├── Services/
│   ├── Documents/
│   │   ├── DocumentAnalysisService.cs      # NEW: Analysis orchestration, Document Agent client
│   │   └── AnalysisSessionService.cs       # NEW: Session CRUD, Cosmos DB operations
│   ├── Chat/
│   │   ├── AnalysisChatService.cs          # NEW: Chat with analysis context, suggestions
│   │   └── ChatHistoryService.cs           # EXISTING: Extend for analysis chat
│   └── Feedback/
│       └── FeedbackService.cs              # NEW: Feedback persistence, aggregation
├── Models/                                   # NEW: DTOs for API contracts
│   ├── DocumentAnalysisRequest.cs
│   ├── DocumentAnalysisResponse.cs
│   ├── AnalysisSessionDto.cs
│   ├── ChatMessageDto.cs
│   └── FeedbackDto.cs
└── appsettings.json                         # EXTEND: Add DocumentAgentApiUrl, DocumentAgentApiKey

# Backend Tests (NEW)
app/SmartFlow.UI.API.Tests/
├── Controllers/
│   ├── DocumentAnalysisControllerTests.cs
│   ├── DocumentChatControllerTests.cs
│   └── FeedbackControllerTests.cs
├── Services/
│   ├── DocumentAnalysisServiceTests.cs
│   ├── AnalysisSessionServiceTests.cs
│   ├── AnalysisChatServiceTests.cs
│   └── FeedbackServiceTests.cs
└── Integration/
    ├── DocumentAnalysisEndpointsTests.cs
    └── ChatEndpointsTests.cs

# Frontend (Blazor WASM)
app/SmartFlow.UI.Client/
├── Pages/
│   ├── DocumentAnalysis.razor              # NEW: Main page (upload + results + chat)
│   ├── DocumentAnalysis.razor.cs
│   ├── AnalysisHistory.razor               # NEW: View past sessions
│   └── AnalysisHistory.razor.cs
├── Components/
│   ├── DocumentUpload.razor                # NEW: Multi-file upload, drag-drop, progress
│   ├── DocumentUpload.razor.cs
│   ├── AnalysisResults.razor               # NEW: Structured data grid, calculations display
│   ├── AnalysisResults.razor.cs
│   ├── DocumentChat.razor                  # NEW: Chat interface with suggested questions
│   ├── DocumentChat.razor.cs
│   ├── FeedbackPanel.razor                 # NEW: Thumbs up/down, comments, accuracy rating
│   ├── FeedbackPanel.razor.cs
│   ├── ExtractedScheduleTable.razor        # NEW: Schedule visualization component
│   └── CalculationResultCard.razor         # NEW: Calculation display component
├── Services/
│   ├── ApiClient.cs                        # EXTEND: Add document analysis, chat, feedback methods
│   └── AnalysisStateService.cs             # NEW: Manage analysis session state across components
└── Models/                                  # NEW: Client-side view models
    ├── AnalysisSessionViewModel.cs
    ├── DocumentUploadViewModel.cs
    └── ChatMessageViewModel.cs

# Frontend Tests (NEW)
app/SmartFlow.UI.Client.Tests/
├── Pages/
│   ├── DocumentAnalysisTests.cs            # bUnit component tests
│   └── AnalysisHistoryTests.cs
├── Components/
│   ├── DocumentUploadTests.cs
│   ├── AnalysisResultsTests.cs
│   ├── DocumentChatTests.cs
│   └── FeedbackPanelTests.cs
└── Services/
    └── AnalysisStateServiceTests.cs

# Shared Models (extend existing)
app/Shared/Shared/
└── Models/
    ├── AnalysisSession.cs                  # NEW: Session entity
    ├── UploadedDocument.cs                 # NEW: Document metadata
    ├── AnalysisResult.cs                   # NEW: Analysis output
    ├── ExtractedSection.cs                 # NEW: Document sections
    ├── ExtractedSchedule.cs                # NEW: Schedule data
    ├── Calculation.cs                      # NEW: Calculation results
    ├── ChatMessage.cs                      # NEW: Chat history
    ├── SuggestedQuestion.cs                # NEW: Suggested questions
    ├── Feedback.cs                         # NEW: Feedback data
    └── UserPermission.cs                   # NEW: Access control
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**:  
Web application structure selected (Blazor WASM frontend + ASP.NET Core backend). Feature extends existing projects:

- **Backend**: `app/SmartFlow.UI.API` - Add 3 controllers, 5 services, 5 DTOs
- **Frontend**: `app/SmartFlow.UI.Client` - Add 2 pages, 7 components, 2 services, 3 view models
- **Shared**: `app/Shared/Shared` - Add 10 entity models
- **Tests**: Create `SmartFlow.UI.API.Tests` (NEW) and `SmartFlow.UI.Client.Tests` (NEW) projects

This maintains consistency with existing SmartFlow UI architecture and follows Blazor WebAssembly best practices (client-side rendering, API proxy pattern, shared models).

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

**No constitutional violations.** All requirements met within existing infrastructure.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| N/A | N/A | N/A |

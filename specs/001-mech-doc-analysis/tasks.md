# Tasks: Mechanical Document Analysis for Sales Engineers

**Input**: Design documents from `/specs/001-mech-doc-analysis/`
**Prerequisites**: plan.md ✅, spec.md ✅

**Tests**: Per Smart Flow UI Constitution Principle II, tests are MANDATORY. Test coverage of 80% for business logic, integration tests for all API endpoints, and bUnit component tests for complex Blazor interactions are required.

**Test-First Development**: Tests MUST be written FIRST, verified to FAIL, then implementation begins (Red-Green-Refactor cycle).

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3, US4)
- Include exact file paths in descriptions

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [X] T001 Create test project structure: `app/SmartFlow.UI.API.Tests/SmartFlow.UI.API.Tests.csproj` with xUnit, Moq, Microsoft.AspNetCore.Mvc.Testing references
- [X] T002 Create test project structure: `app/SmartFlow.UI.Client.Tests/SmartFlow.UI.Client.Tests.csproj` with bUnit, xUnit, Moq references
- [X] T003 [P] Add test projects to solution file `app/SmartFlow.UI.sln`
- [X] T004 [P] Configure test project settings: enable nullable, set LangVersion=12, add Directory.Packages.props reference
- [X] T005 [P] Create test base classes: `app/SmartFlow.UI.API.Tests/Infrastructure/WebApplicationFactoryBase.cs` for integration tests
- [X] T006 [P] Create test base classes: `app/SmartFlow.UI.Client.Tests/Infrastructure/BunitTestContext.cs` for component tests

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

**Note**: Includes global exception handler middleware for FR-020 compliance (graceful error handling across all API endpoints)

- [X] T007 Add Document Agent configuration to `app/SmartFlow.UI.API/AppConfiguration.cs`: DocumentAgentApiUrl, DocumentAgentApiKey properties
- [X] T008 [P] Create Cosmos DB container configuration for AnalysisSessions collection in `app/SmartFlow.UI.API/Extensions/ServiceCollectionExtensions.cs`
- [X] T009 [P] Create Cosmos DB container configuration for ChatMessages collection in `app/SmartFlow.UI.API/Extensions/ServiceCollectionExtensions.cs`
- [X] T010 Create Azure Blob Storage container helper for mech-doc-analysis in `app/SmartFlow.UI.API/Services/AzureBlobStorageService.cs`
- [X] T011 [P] Create shared entity models in `app/Shared/Shared/Models/AnalysisSession.cs` with XML docs
- [X] T012 [P] Create shared entity models in `app/Shared/Shared/Models/UploadedDocument.cs` with XML docs
- [X] T013 [P] Create shared entity models in `app/Shared/Shared/Models/AnalysisResult.cs` with XML docs
- [X] T014 [P] Create shared entity models in `app/Shared/Shared/Models/ExtractedSection.cs` with XML docs
- [X] T015 [P] Create shared entity models in `app/Shared/Shared/Models/ExtractedSchedule.cs` with XML docs
- [X] T016 [P] Create shared entity models in `app/Shared/Shared/Models/Calculation.cs` with XML docs
- [X] T017 [P] Create shared entity models in `app/Shared/Shared/Models/ChatMessage.cs` with XML docs
- [X] T018 [P] Create shared entity models in `app/Shared/Shared/Models/SuggestedQuestion.cs` with XML docs
- [X] T019 [P] Create shared entity models in `app/Shared/Shared/Models/Feedback.cs` with XML docs
- [X] T020 [P] Create shared entity models in `app/Shared/Shared/Models/UserPermission.cs` with XML docs
- [X] T021 Register Document Agent HTTP client in `app/SmartFlow.UI.API/Extensions/ServiceCollectionExtensions.cs` with retry policy
- [X] T022 Add navigation menu item for Document Analysis in `app/SmartFlow.UI.Client/Shared/NavMenu.razor`
- [X] T023 [P] Configure Application Insights custom events for document analysis operations
- [X] T024 [P] Add health check for Document Agent API connectivity in `app/SmartFlow.UI.API/Services/HealthChecks/DocumentAgentHealthCheck.cs`
- [X] T024a Implement global exception handler middleware in `app/SmartFlow.UI.API/Middleware/GlobalExceptionHandler.cs` with structured error responses, logging, correlation IDs
- [X] T024b [P] Unit tests for GlobalExceptionHandler in `app/SmartFlow.UI.API.Tests/Middleware/GlobalExceptionHandlerTests.cs` (test exception types, status codes, error formatting)

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Multi-Document Upload and Basic Analysis (Priority: P1) 🎯 MVP

**Goal**: Enable sales engineers to upload multiple PDFs, trigger analysis via Document Agent, and view structured results with summary

**Independent Test**: Upload single mechanical spec PDF, trigger analysis, receive summary with extracted sections and schedules

### Tests for User Story 1 (MANDATORY per Constitution) 🔴 RED

> **CRITICAL: Write these tests FIRST, run them, ensure they FAIL before implementation begins**

- [ ] T025 [P] [US1] Unit tests for AnalysisSession model validation in `app/SmartFlow.UI.API.Tests/Models/AnalysisSessionTests.cs`
- [ ] T026 [P] [US1] Unit tests for UploadedDocument model validation in `app/SmartFlow.UI.API.Tests/Models/UploadedDocumentTests.cs`
- [ ] T027 [P] [US1] Unit tests for DocumentAnalysisService in `app/SmartFlow.UI.API.Tests/Services/DocumentAnalysisServiceTests.cs`
- [ ] T028 [P] [US1] Unit tests for AnalysisSessionService in `app/SmartFlow.UI.API.Tests/Services/AnalysisSessionServiceTests.cs`
- [ ] T029 [P] [US1] Integration test for DocumentAnalysisController POST /api/analysis/upload in `app/SmartFlow.UI.API.Tests/Integration/DocumentAnalysisEndpointsTests.cs`
- [ ] T030 [P] [US1] Integration test for DocumentAnalysisController GET /api/analysis/{sessionId} in `app/SmartFlow.UI.API.Tests/Integration/DocumentAnalysisEndpointsTests.cs`
- [ ] T031 [P] [US1] Integration test for DocumentAnalysisController GET /api/analysis/{sessionId}/status in `app/SmartFlow.UI.API.Tests/Integration/DocumentAnalysisEndpointsTests.cs`
- [ ] T032 [P] [US1] Component test for DocumentUpload.razor with file selection in `app/SmartFlow.UI.Client.Tests/Components/DocumentUploadTests.cs`
- [ ] T033 [P] [US1] Component test for DocumentUpload.razor with drag-drop in `app/SmartFlow.UI.Client.Tests/Components/DocumentUploadTests.cs`
- [ ] T034 [P] [US1] Component test for AnalysisResults.razor with mock data in `app/SmartFlow.UI.Client.Tests/Components/AnalysisResultsTests.cs`
- [ ] T035 [P] [US1] Component test for ExtractedScheduleTable.razor in `app/SmartFlow.UI.Client.Tests/Components/ExtractedScheduleTableTests.cs`
- [ ] T036 [US1] Verify all tests FAIL (no implementation exists yet) - commit failing tests

### Implementation for User Story 1 🟢 GREEN

> **Only begin implementation after tests are written and failing**

- [ ] T037 [P] [US1] Create DocumentAnalysisRequest DTO in `app/SmartFlow.UI.API/Models/DocumentAnalysisRequest.cs` with XML docs
- [ ] T038 [P] [US1] Create DocumentAnalysisResponse DTO in `app/SmartFlow.UI.API/Models/DocumentAnalysisResponse.cs` with XML docs
- [ ] T039 [P] [US1] Create AnalysisSessionDto in `app/SmartFlow.UI.API/Models/AnalysisSessionDto.cs` with XML docs
- [ ] T040 [US1] Implement DocumentAnalysisService in `app/SmartFlow.UI.API/Services/Documents/DocumentAnalysisService.cs` with Document Agent HTTP client, logging, error handling
- [ ] T041 [US1] Implement AnalysisSessionService in `app/SmartFlow.UI.API/Services/Documents/AnalysisSessionService.cs` with Cosmos DB CRUD operations
- [ ] T042 [US1] Implement DocumentAnalysisController in `app/SmartFlow.UI.API/Controllers/DocumentAnalysisController.cs` with upload, status, get session endpoints
- [ ] T043 [US1] Register services in DI container in `app/SmartFlow.UI.API/Extensions/ServiceCollectionExtensions.cs`
- [ ] T044 [US1] Map API endpoints in `app/SmartFlow.UI.API/Program.cs` or create extension method
- [ ] T045 [P] [US1] Create AnalysisSessionViewModel in `app/SmartFlow.UI.Client/Models/AnalysisSessionViewModel.cs`
- [ ] T046 [P] [US1] Create DocumentUploadViewModel in `app/SmartFlow.UI.Client/Models/DocumentUploadViewModel.cs`
- [ ] T047 [US1] Extend ApiClient with UploadDocumentsForAnalysisAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T048 [US1] Extend ApiClient with GetAnalysisSessionAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T049 [US1] Extend ApiClient with GetAnalysisStatusAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T050 [US1] Create AnalysisStateService in `app/SmartFlow.UI.Client/Services/AnalysisStateService.cs` for session state management
- [ ] T051 [US1] Implement DocumentUpload.razor component in `app/SmartFlow.UI.Client/Components/DocumentUpload.razor` with MudFileUpload, drag-drop, progress
- [ ] T052 [US1] Implement DocumentUpload.razor.cs code-behind with upload logic, validation, error handling
- [ ] T053 [US1] Implement AnalysisResults.razor component in `app/SmartFlow.UI.Client/Components/AnalysisResults.razor` with MudDataGrid, expandable sections
- [ ] T054 [US1] Implement AnalysisResults.razor.cs code-behind with result rendering, detail navigation
- [ ] T055 [US1] Implement ExtractedScheduleTable.razor component in `app/SmartFlow.UI.Client/Components/ExtractedScheduleTable.razor` with MudTable
- [ ] T056 [US1] Implement ExtractedScheduleTable.razor.cs code-behind
- [ ] T057 [US1] Implement CalculationResultCard.razor component in `app/SmartFlow.UI.Client/Components/CalculationResultCard.razor` with MudCard
- [ ] T058 [US1] Implement CalculationResultCard.razor.cs code-behind
- [ ] T059 [US1] Implement DocumentAnalysis.razor page in `app/SmartFlow.UI.Client/Pages/DocumentAnalysis.razor` integrating upload, results, status polling
- [ ] T060 [US1] Implement DocumentAnalysis.razor.cs page code-behind with lifecycle, state management
- [ ] T061 [US1] Add file type validation (PDF only) to upload component
- [ ] T062 [US1] Add file size validation (50MB limit) to upload component
- [ ] T063 [US1] Add loading states (MudProgressCircular, MudProgressLinear) for async operations
- [ ] T064 [US1] Add user-facing error messages with MudSnackbar for upload/analysis failures
- [ ] T065 [US1] Add retry mechanism for failed Document Agent requests (3 retries with exponential backoff)
- [ ] T066 [US1] Add structured logging with correlation IDs for all Document Agent calls
- [ ] T067 [US1] Add Application Insights custom events for upload start/complete/fail, analysis start/complete/fail
- [ ] T068 [US1] Verify all tests PASS - commit working implementation

### Refactor for User Story 1 🔵 REFACTOR

> **Clean up implementation while keeping tests green**

- [ ] T069 [US1] Code review: verify SRP (each service/component has one clear purpose)
- [ ] T070 [US1] Code review: verify DI (no hard-coded dependencies, all services injected)
- [ ] T071 [US1] Code review: verify type safety (nullable reference types, no dynamic/object abuse)
- [ ] T072 [US1] Code review: verify XML documentation on all public APIs
- [ ] T073 [US1] Verify test coverage ≥80% for DocumentAnalysisService, AnalysisSessionService
- [ ] T074 [US1] Performance check: Upload API responds within 200ms, status polling within 300ms
- [ ] T075 [US1] Performance check: AnalysisResults component renders within 100ms
- [ ] T076 [US1] Accessibility check: keyboard navigation for file upload, results navigation
- [ ] T077 [US1] Accessibility check: screen reader announcements for upload progress, analysis status
- [ ] T078 [US1] Accessibility check: ARIA labels for upload zone, file list, result cards
- [ ] T079 [US1] Responsive design check: test upload/results on mobile (≥375px), tablet (≥768px), desktop (≥1024px)
- [ ] T080 [US1] Security check: [Authorize] attribute on DocumentAnalysisController endpoints
- [ ] T081 [US1] Security check: Document Agent API key in Key Vault, not config file

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently. Sales engineer can upload PDFs, trigger analysis, and view structured results.

---

## Phase 4: User Story 2 - Interactive Chat with Suggested Follow-ups (Priority: P2)

**Goal**: Enable sales engineers to ask questions about analyzed documents via chat interface with auto-generated suggested questions

**Independent Test**: Complete document analysis (US1), then ask "What is the HVAC tonnage requirement?" and receive answer with suggested follow-ups

### Tests for User Story 2 (MANDATORY per Constitution) 🔴 RED

> **CRITICAL: Write these tests FIRST, run them, ensure they FAIL before implementation begins**

- [ ] T082 [P] [US2] Unit tests for ChatMessage model validation in `app/SmartFlow.UI.API.Tests/Models/ChatMessageTests.cs`
- [ ] T083 [P] [US2] Unit tests for SuggestedQuestion model in `app/SmartFlow.UI.API.Tests/Models/SuggestedQuestionTests.cs`
- [ ] T084 [P] [US2] Unit tests for AnalysisChatService in `app/SmartFlow.UI.API.Tests/Services/AnalysisChatServiceTests.cs`
- [ ] T085 [P] [US2] Integration test for DocumentChatController POST /api/chat/{sessionId}/message in `app/SmartFlow.UI.API.Tests/Integration/ChatEndpointsTests.cs`
- [ ] T086 [P] [US2] Integration test for DocumentChatController GET /api/chat/{sessionId}/history in `app/SmartFlow.UI.API.Tests/Integration/ChatEndpointsTests.cs`
- [ ] T087 [P] [US2] Integration test for DocumentChatController GET /api/chat/{sessionId}/suggestions in `app/SmartFlow.UI.API.Tests/Integration/ChatEndpointsTests.cs`
- [ ] T088 [P] [US2] Component test for DocumentChat.razor with message send in `app/SmartFlow.UI.Client.Tests/Components/DocumentChatTests.cs`
- [ ] T089 [P] [US2] Component test for DocumentChat.razor with suggested question click in `app/SmartFlow.UI.Client.Tests/Components/DocumentChatTests.cs`
- [ ] T090 [US2] Verify all tests FAIL - commit failing tests

### Implementation for User Story 2 🟢 GREEN

- [ ] T091 [P] [US2] Create ChatMessageDto in `app/SmartFlow.UI.API/Models/ChatMessageDto.cs` with XML docs
- [ ] T092 [P] [US2] Create ChatMessageViewModel in `app/SmartFlow.UI.Client/Models/ChatMessageViewModel.cs`
- [ ] T093 [US2] Implement AnalysisChatService in `app/SmartFlow.UI.API/Services/Chat/AnalysisChatService.cs` with Document Agent integration, context retrieval
- [ ] T094 [US2] Extend ChatHistoryService in `app/SmartFlow.UI.API/Services/ChatHistory/ChatHistoryService.cs` to support analysis chat (add methods for session-specific history)
- [ ] T095 [US2] Implement DocumentChatController in `app/SmartFlow.UI.API/Controllers/DocumentChatController.cs` with send message, get history, get suggestions endpoints
- [ ] T096 [US2] Register chat services in DI container in `app/SmartFlow.UI.API/Extensions/ServiceCollectionExtensions.cs`
- [ ] T097 [US2] Extend ApiClient with SendChatMessageAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T098 [US2] Extend ApiClient with GetChatHistoryAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T099 [US2] Extend ApiClient with GetSuggestedQuestionsAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T100 [US2] Implement DocumentChat.razor component in `app/SmartFlow.UI.Client/Components/DocumentChat.razor` with MudTextField, message list, suggested questions
- [ ] T101 [US2] Implement DocumentChat.razor.cs code-behind with message sending, streaming response, history loading
- [ ] T102 [US2] Integrate DocumentChat component into DocumentAnalysis.razor page (add to layout, show after analysis complete)
- [ ] T103 [US2] Implement streaming chat responses (progressive word-by-word rendering with SignalR or SSE)
- [ ] T104 [US2] Add typing indicator (MudProgressCircular) while waiting for chat response
- [ ] T105 [US2] Add automatic suggested question generation after initial analysis completes (3-5 questions)
- [ ] T106 [US2] Add automatic follow-up question generation after each chat response (2-3 questions)
- [ ] T107 [US2] Add document citation rendering in chat messages (page/section references as links)
- [ ] T108 [US2] Add chat history persistence in Cosmos DB via ChatHistoryService
- [ ] T109 [US2] Add loading state for chat message send (disable input, show spinner)
- [ ] T110 [US2] Add error handling for chat failures (Snackbar notification, retry button)
- [ ] T111 [US2] Add structured logging for chat interactions (message sent, response received, suggestions generated)
- [ ] T112 [US2] Add Application Insights custom events for chat operations
- [ ] T113 [US2] Verify all tests PASS - commit working implementation

### Refactor for User Story 2 🔵 REFACTOR

- [ ] T114 [US2] Code review: SRP, DI, type safety, XML docs
- [ ] T115 [US2] Verify test coverage ≥80% for AnalysisChatService
- [ ] T116 [US2] Performance check: Chat response streaming begins within 500ms
- [ ] T117 [US2] Performance check: DocumentChat component renders new message within 50ms
- [ ] T118 [US2] Accessibility check: keyboard navigation for chat input, suggested question buttons
- [ ] T119 [US2] Accessibility check: screen reader support for new messages, suggested questions
- [ ] T120 [US2] Accessibility check: ARIA live region for chat message arrivals
- [ ] T121 [US2] Responsive design check: chat interface on mobile/tablet/desktop
- [ ] T122 [US2] Security check: [Authorize] on DocumentChatController, session ownership validation

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently. Sales engineer can upload docs, analyze, and chat with contextual suggestions.

---

## Phase 5: User Story 3 - User Feedback Collection (Priority: P3)

**Goal**: Enable sales engineers to provide thumbs up/down feedback on analysis quality, with privileged users providing detailed written feedback

**Independent Test**: Complete any analysis or chat interaction, click thumbs up/down button, verify feedback recorded. For privileged users, provide detailed feedback text.

### Tests for User Story 3 (MANDATORY per Constitution) 🔴 RED

- [ ] T123 [P] [US3] Unit tests for Feedback model validation in `app/SmartFlow.UI.API.Tests/Models/FeedbackTests.cs`
- [ ] T124 [P] [US3] Unit tests for FeedbackService in `app/SmartFlow.UI.API.Tests/Services/FeedbackServiceTests.cs`
- [ ] T125 [P] [US3] Integration test for FeedbackController POST /api/feedback in `app/SmartFlow.UI.API.Tests/Integration/FeedbackControllerTests.cs`
- [ ] T126 [P] [US3] Integration test for FeedbackController GET /api/feedback/{sessionId} in `app/SmartFlow.UI.API.Tests/Integration/FeedbackControllerTests.cs`
- [ ] T127 [P] [US3] Component test for FeedbackPanel.razor with thumbs up/down in `app/SmartFlow.UI.Client.Tests/Components/FeedbackPanelTests.cs`
- [ ] T128 [P] [US3] Component test for FeedbackPanel.razor with detailed text submission in `app/SmartFlow.UI.Client.Tests/Components/FeedbackPanelTests.cs`
- [ ] T129 [US3] Verify all tests FAIL - commit failing tests

### Implementation for User Story 3 🟢 GREEN

- [ ] T130 [P] [US3] Create FeedbackDto in `app/SmartFlow.UI.API/Models/FeedbackDto.cs` with XML docs
- [ ] T131 [US3] Implement FeedbackService in `app/SmartFlow.UI.API/Services/Feedback/FeedbackService.cs` with Cosmos DB persistence, aggregation
- [ ] T132 [US3] Implement FeedbackController in `app/SmartFlow.UI.API/Controllers/FeedbackController.cs` with submit, get endpoints
- [ ] T133 [US3] Register FeedbackService in DI container in `app/SmartFlow.UI.API/Extensions/ServiceCollectionExtensions.cs`
- [ ] T134 [US3] Extend ApiClient with SubmitFeedbackAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T135 [US3] Extend ApiClient with GetFeedbackForSessionAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T136 [US3] Implement FeedbackPanel.razor component in `app/SmartFlow.UI.Client/Components/FeedbackPanel.razor` with MudIconButton (thumbs up/down), MudTextField (detailed)
- [ ] T137 [US3] Implement FeedbackPanel.razor.cs code-behind with submission logic, privilege check, validation
- [ ] T138 [US3] Integrate FeedbackPanel into AnalysisResults.razor component (show after results displayed)
- [ ] T139 [US3] Integrate FeedbackPanel into DocumentChat.razor component (show after each chat response)
- [ ] T140 [US3] Add user permission check for detailed feedback capability (UserPermission entity)
- [ ] T141 [US3] Add feedback text validation (max 1000 characters) with real-time counter
- [ ] T142 [US3] Add confirmation message after feedback submission (Snackbar notification)
- [ ] T143 [US3] Add feedback central storage endpoint for project team review (separate admin API or export)
- [ ] T144 [US3] Add structured logging for feedback submissions (user ID, session ID, rating, timestamp)
- [ ] T145 [US3] Add Application Insights custom events for feedback operations
- [ ] T146 [US3] Verify all tests PASS - commit working implementation

### Refactor for User Story 3 🔵 REFACTOR

- [ ] T147 [US3] Code review: SRP, DI, type safety, XML docs
- [ ] T148 [US3] Verify test coverage ≥80% for FeedbackService
- [ ] T149 [US3] Performance check: Feedback submission responds within 300ms
- [ ] T150 [US3] Performance check: FeedbackPanel renders within 50ms
- [ ] T151 [US3] Accessibility check: keyboard navigation for thumbs buttons, text input
- [ ] T152 [US3] Accessibility check: screen reader support for feedback confirmation
- [ ] T153 [US3] Accessibility check: ARIA labels for thumbs up/down buttons
- [ ] T154 [US3] Responsive design check: feedback panel on mobile/tablet/desktop
- [ ] T155 [US3] Security check: [Authorize] on FeedbackController, user permission validation

**Checkpoint**: All core user stories (US1, US2, US3) should now be independently functional. Feedback collection enhances the experience without breaking core analysis/chat.

---

## Phase 6: User Story 4 - Analysis Result Management and Export (Priority: P3)

**Goal**: Enable sales engineers to view analysis history, restore previous sessions, and export results as PDF or JSON

**Independent Test**: Complete multiple document analyses over time, view history list, select previous session, export results as PDF

### Tests for User Story 4 (MANDATORY per Constitution) 🔴 RED

- [ ] T156 [P] [US4] Integration test for DocumentAnalysisController GET /api/analysis/history in `app/SmartFlow.UI.API.Tests/Integration/DocumentAnalysisEndpointsTests.cs`
- [ ] T157 [P] [US4] Integration test for DocumentAnalysisController GET /api/analysis/{sessionId}/export/pdf in `app/SmartFlow.UI.API.Tests/Integration/DocumentAnalysisEndpointsTests.cs`
- [ ] T158 [P] [US4] Integration test for DocumentAnalysisController GET /api/analysis/{sessionId}/export/json in `app/SmartFlow.UI.API.Tests/Integration/DocumentAnalysisEndpointsTests.cs`
- [ ] T159 [P] [US4] Component test for AnalysisHistory.razor with session list in `app/SmartFlow.UI.Client.Tests/Pages/AnalysisHistoryTests.cs`
- [ ] T160 [P] [US4] Component test for AnalysisHistory.razor with session selection in `app/SmartFlow.UI.Client.Tests/Pages/AnalysisHistoryTests.cs`
- [ ] T161 [US4] Verify all tests FAIL - commit failing tests

### Implementation for User Story 4 🟢 GREEN

- [ ] T162 [US4] Add GetAnalysisHistoryAsync method to AnalysisSessionService in `app/SmartFlow.UI.API/Services/Documents/AnalysisSessionService.cs` (Cosmos DB query with pagination)
- [ ] T163 [US4] Add ExportToPdfAsync method to DocumentAnalysisService in `app/SmartFlow.UI.API/Services/Documents/DocumentAnalysisService.cs` (generate PDF report)
- [ ] T164 [US4] Add ExportToJsonAsync method to DocumentAnalysisService in `app/SmartFlow.UI.API/Services/Documents/DocumentAnalysisService.cs` (serialize structured data)
- [ ] T165 [US4] Add history, export endpoints to DocumentAnalysisController in `app/SmartFlow.UI.API/Controllers/DocumentAnalysisController.cs`
- [ ] T166 [US4] Extend ApiClient with GetAnalysisHistoryAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T167 [US4] Extend ApiClient with ExportAnalysisAsPdfAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T168 [US4] Extend ApiClient with ExportAnalysisAsJsonAsync in `app/SmartFlow.UI.Client/Services/ApiClient.cs`
- [ ] T169 [US4] Implement AnalysisHistory.razor page in `app/SmartFlow.UI.Client/Pages/AnalysisHistory.razor` with MudTable, pagination, search
- [ ] T170 [US4] Implement AnalysisHistory.razor.cs page code-behind with history loading, session selection navigation
- [ ] T171 [US4] Add session restoration logic to DocumentAnalysis.razor (load full results + chat history when sessionId query param present)
- [ ] T172 [US4] Add export button to AnalysisResults component with format selection dropdown (PDF, JSON)
- [ ] T173 [US4] Add export download handling (trigger browser download with correct filename, content-type)
- [ ] T174 [US4] Add pagination for history view (max 20 sessions per page with MudPagination)
- [ ] T175 [US4] Add search/filter for history (by document name, date range)
- [ ] T176 [US4] Add loading state for history page (skeleton loader with MudProgressLinear)
- [ ] T177 [US4] Add error handling for history load failures (Snackbar notification)
- [ ] T178 [US4] Add structured logging for export operations (format, session ID, file size)
- [ ] T179 [US4] Add Application Insights custom events for history view, export operations
- [ ] T180 [US4] Verify all tests PASS - commit working implementation

### Refactor for User Story 4 🔵 REFACTOR

- [ ] T181 [US4] Code review: SRP, DI, type safety, XML docs
- [ ] T182 [US4] Verify test coverage ≥80% for export methods, history queries
- [ ] T183 [US4] Performance check: History page loads within 2 seconds
- [ ] T184 [US4] Performance check: Export generates within 3 seconds for typical session
- [ ] T185 [US4] Accessibility check: keyboard navigation for history table, export buttons
- [ ] T186 [US4] Accessibility check: screen reader support for history list, export confirmation
- [ ] T187 [US4] Responsive design check: history table on mobile/tablet/desktop (collapse columns on mobile)
- [ ] T188 [US4] Security check: [Authorize] on history/export endpoints, user ownership validation

**Checkpoint**: All user stories (US1-US4) should now be independently functional. Full feature set complete with upload, analysis, chat, feedback, history, and export.

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T189 [P] Update README.md with feature overview, getting started guide, Document Agent configuration
- [ ] T190 [P] Add XML documentation comments review pass (ensure all public APIs documented)
- [ ] T191 [P] Code cleanup: remove dead code, unused imports, commented code
- [ ] T192 [P] Code cleanup: verify consistent naming conventions (PascalCase classes, camelCase variables)
- [ ] T193 Performance profiling: analyze API endpoint response times under load (50 concurrent users)
- [ ] T194 Performance optimization: add response caching for suggested questions (5-minute TTL)
- [ ] T195 Performance optimization: add blob storage caching for frequently accessed documents
- [ ] T196 [P] Security audit: run dependency vulnerability scan with dotnet list package --vulnerable
- [ ] T197 [P] Security audit: verify all secrets in Key Vault (Document Agent API key, storage connection strings)
- [ ] T198 [P] Security audit: verify RBAC enforcement on all endpoints (no authorization bypasses)
- [ ] T199 Accessibility audit: run axe DevTools on all pages (DocumentAnalysis, AnalysisHistory)
- [ ] T200 Accessibility audit: verify WCAG 2.1 Level AA compliance (color contrast, focus indicators, ARIA)
- [ ] T201 Accessibility audit: test full keyboard navigation flow (no mouse required)
- [ ] T202 Accessibility audit: test screen reader flow with NVDA/JAWS
- [ ] T203 Test coverage review: verify ≥80% for all business logic (DocumentAnalysisService, AnalysisChatService, FeedbackService, AnalysisSessionService)
- [ ] T204 Test coverage review: verify all API endpoints have integration tests
- [ ] T205 Test coverage review: verify all Blazor components with interactions have bUnit tests
- [ ] T206 CI/CD pipeline verification: ensure build treats warnings as errors
- [ ] T207 CI/CD pipeline verification: ensure test gates fail build on any test failure
- [ ] T208 CI/CD pipeline verification: ensure coverage reports generated and published
- [ ] T209 [P] Add Application Insights dashboard for document analysis operations (upload count, analysis success rate, chat usage)
- [ ] T210 [P] Add Application Insights alerts for p95 >2s, 5xx error rate >1%, Document Agent health check failures
- [ ] T211 User acceptance testing: validate complete upload → analyze → chat → feedback → export workflow
- [ ] T212 User acceptance testing: validate analysis history view and session restoration
- [ ] T213 Load testing: simulate 50 concurrent users uploading documents
- [ ] T214 Load testing: validate no memory leaks during sustained chat conversations
- [ ] T215 Edge case testing: corrupted PDF, oversized file, network timeout during upload, Document Agent unavailable
- [ ] T216 Edge case testing: non-English documents, mixed language documents, scanned images without text

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3-6)**: All depend on Foundational phase completion
  - User stories can then proceed in parallel (if staffed) or sequentially in priority order (P1 → P2 → P3)
- **Polish (Phase 7)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) - No dependencies on other stories
- **User Story 2 (P2)**: Can start after Foundational (Phase 2) - Integrates with US1 (DocumentAnalysis page) but independently testable
- **User Story 3 (P3)**: Can start after Foundational (Phase 2) - Integrates with US1/US2 (AnalysisResults, DocumentChat) but independently testable
- **User Story 4 (P3)**: Can start after Foundational (Phase 2) - Depends on US1 for session concept, but independently testable

### Within Each User Story (Red-Green-Refactor Cycle)

1. **RED Phase**: Write all tests FIRST, verify they FAIL, commit failing tests
2. **GREEN Phase**: Implement minimal code to make tests pass, verify SUCCESS, commit working code
3. **REFACTOR Phase**: Clean up code (SRP, naming, patterns), verify tests still pass, check coverage/performance/accessibility

- Tests MUST be written and FAIL before implementation
- Shared entity models (Phase 2) before services
- Services before controllers
- Controllers before client components
- Components before pages
- Integration last within story

### Parallel Opportunities

- **Setup (Phase 1)**: T001-T006 can all run in parallel (different test projects)
- **Foundational (Phase 2)**: T008-T020 entity models can all run in parallel, T023-T024 health checks can run in parallel
- **User Story Tests**: All test tasks marked [P] within a story can run in parallel
- **User Story Models/DTOs**: All DTO creation tasks marked [P] can run in parallel (T037-T039, T045-T046, T091-T092, T130)
- **Multiple User Stories**: Once Foundational complete, US1, US2, US3, US4 can all be worked on in parallel by different team members

---

## Parallel Example: User Story 1

```bash
# Phase 1: Write all tests in parallel (RED):
T025: Unit tests for AnalysisSession model
T026: Unit tests for UploadedDocument model
T027: Unit tests for DocumentAnalysisService
T028: Unit tests for AnalysisSessionService
T029-T035: Integration and component tests

# Phase 2: Create all DTOs in parallel (GREEN):
T037: DocumentAnalysisRequest DTO
T038: DocumentAnalysisResponse DTO
T039: AnalysisSessionDto
T045: AnalysisSessionViewModel
T046: DocumentUploadViewModel
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001-T006)
2. Complete Phase 2: Foundational (T007-T024) - CRITICAL, blocks all stories
3. Complete Phase 3: User Story 1 (T025-T081)
4. **STOP and VALIDATE**: Test User Story 1 independently - upload PDF, trigger analysis, view results
5. Deploy/demo MVP if ready

**MVP Delivers**: Sales engineers can upload mechanical spec PDFs, trigger Document Agent analysis, and view structured results with summaries. This is the core value proposition.

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add User Story 1 (T025-T081) → Test independently → Deploy/Demo (MVP! 🎯)
3. Add User Story 2 (T082-T122) → Test independently → Deploy/Demo (Chat capability added)
4. Add User Story 3 (T123-T155) → Test independently → Deploy/Demo (Feedback mechanism added)
5. Add User Story 4 (T156-T188) → Test independently → Deploy/Demo (History and export added)
6. Polish (T189-T216) → Final validation → Production release

Each story adds value without breaking previous stories.

### Parallel Team Strategy

With 4 developers available:

1. **All together**: Complete Setup (Phase 1) + Foundational (Phase 2)
2. **Once Foundational is done**, split into parallel tracks:
   - Developer A: User Story 1 (T025-T081)
   - Developer B: User Story 2 (T082-T122)
   - Developer C: User Story 3 (T123-T155)
   - Developer D: User Story 4 (T156-T188)
3. **Integration**: Stories integrate naturally (US2 adds chat to US1 page, US3 adds feedback to US1/US2 components, US4 adds history page)
4. **Final together**: Polish (Phase 7)

Stories complete and integrate independently, minimizing merge conflicts.

---

## Notes

- **[P] tasks**: Different files, no dependencies - can run in parallel
- **[Story] label**: Maps task to specific user story for traceability
- **Each user story**: Independently completable and testable per spec.md acceptance criteria
- **Test-first**: Verify tests fail (RED) before implementing (GREEN)
- **Commit frequency**: After each task or logical group (test suite, service, component)
- **Checkpoints**: Stop at each checkpoint to validate story independently before proceeding
- **Constitution compliance**: All tasks designed to meet 80% coverage, WCAG 2.1 AA, performance budgets, security requirements
- **File paths**: All paths use forward slashes for cross-platform compatibility
- **Dependencies**: Minimize cross-story dependencies - each story should be independently valuable

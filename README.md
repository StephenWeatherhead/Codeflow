# Codeflow
The aim is to develop a tool to convert Workflow Foundation workflows to C# code on .NET 8. The point is to preserve control-flow logic developed with XAML workflows without manually recreating them which is time-intensive and error-prone. And also by converting them to code we can make changes to the logic (unlike libraries that are a port of WF to .NET 8).

The proposed solution is something like this:

1. Develop a runtime library that mimics the behaviour of Workflow Foundation activities. This will be used to make one-to-one conversions in code-generation.
2. Develop a tool that can take Visual Basic expressions and convert them into a library that can be called from C# code. Using a Visual Basic library, we can avoid Visual Basic expression compilation which would complicate the runtime library and incur performance costs. Since we know the code identifiers (e.g. Variables, InArguments) when we're converting the XAML, we should be able to generate a static Visual Basic method for an expression that takes just the required parameters for the expression. Easier said than done of course.
3. Develop an API to re-compile custom Code Activities to work with the runtime library. This won't work with custom Native Activities but we're assuming that most custom activities are Code Activities.
4. Develop a tool that reads XAML workflows and generates equivalent VB code and C# code using the runtime library.

Using this method, we don't need to reference Workflow Foundation, third-party scripting engines, or third-party workflow engines.

## General Checklist
- [ ] Visual Basic proof-of-concept - can VB code actually be called from C#? Develop a method that takes a visual basic expression and a list of parameters/types and generates a Visual Basic method from it.
- [ ] Simple workflow proof-of-concept - convert a simple WriteLine workflow to code
- [ ] CodeActivity proof-of-concept

## Runtime Checklist

### Control Flow
- [ ] DoWhile
- [ ] ForEach
- [ ] If
- [ ] Parallel
- [ ] ParallelForEach
- [ ] Pick
- [ ] Sequence
- [ ] Switch
- [ ] While

### Flowchart
- [ ] Flowchart
- [ ] FlowDecision
- [ ] FlowSwitch

### Runtime
- [ ] TerminateWorkflow

### Primitives
- [ ] Assign
- [ ] Delay
- [ ] InvokeDelegate
- [ ] InvokeMethod
- [ ] WriteLine

### Collection
- [ ] AddToCollection
- [ ] ClearCollection
- [ ] ExistsInCollection
- [ ] RemoveFromCollection

### Error Handling
- [ ] Rethrow
- [ ] Throw
- [ ] TryCatch




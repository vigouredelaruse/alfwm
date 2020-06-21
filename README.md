mrgnc => triage => release

# Introduction 
current modernity in system design is best left with the concise description, complexity

i am developing an application that runs on windows (at first) and integrates data flows
from hetregenous sources while permitting random users to conduct analytics on pipelines
consructed from these random data flows

the requirement for something like this will lead you towards providing a palette of 
data sources, a palette of operations, and a mechanism for property mapping and projection
of arbitrary objects

additionally the ingress and egress operations need to be schedulable and filterable,
re-authenticate semi-autonomously, expose a human readable log etc

long before the clown wars looked obsolete one would have approached the problem 
with a few asynchronous interfaces and a service oriented architecture

but autonomous services need autonomous management. that's where this effort comes in

this is not a rebel open source full featured workflow manager. it's a low footprint workflow
manager for coders. 

enjoy

# Getting Started
Current state as of the timestamp on the document: 
  * building towards supporting flowchart type process definitions, with bpm obviously in mind
  * nothing resembling running code with a 'built-in' gui currently in sight
  * major wins, specification of an 'active compound document' subsystem wherein ingressed data 
    is 'effectively' typed and composable with attachable UI behaviour
    
    that is, tuples expose their properties, can exist in mixed collections, and contain
    type information about their assignable consumers
    
    that is, a taxonomy has been defined based around process definitions 
    that contain pipelines
    that contain pipeline tools
    that can contain other pipeline tools
    with current archetypes of Pipeline, Workflow and Activity
    supported by Func<x,x> userSuppliedPredicates for execution tree scheduling decisions
    
For now, look at the code, maybe try compiling it and implementing the interfaces and extending 
the abstract classes and finding the pain points.

# Build and Test
Use Visual Studio 

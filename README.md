# Standard Chain

A trivial generalised de-centralised blockchain implementation in C# .Net
Standard.

Based on https://hackernoon.com/learn-blockchains-by-building-one-117428612f46

## Usage

The intended usage is in a centralised server architecture where multiple
clients may want to add transactions to a chain - aka multiple RESTful web
clients changing the workflow state of a single item.

## Consensus

The consensus algorithm makes the longest blockchain the winner.

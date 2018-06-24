# Standard Chain

A trivial generalised de-centralised blockchain implementation in C# .Net
Standard.

Based on https://hackernoon.com/learn-blockchains-by-building-one-117428612f46

This is a blockchain domain model, not a blockchain service.

## Usage

The intended usage is in a centralised server architecture where multiple
clients may want to add transactions to a chain - aka multiple RESTful web
clients changing the workflow state of a single item.

## Consensus

The consensus algorithm makes the longest blockchain the winner.

AKA if someone has added a transaction to the central blockchain since you
retrieved it, they win. You have to re-apply your transaction.

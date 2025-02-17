module EvoNet.Simulation

open System
open Agent
open EvoNet.Crossover
open EvoNet.Mutation
open EvoNet.Selection
open Gene
open Neurons
open World

let generateGene () =
    {
        Source = randomSource ()
        Dest = randomDest ()
        Weight = Random.Shared.NextSingle() * 2f - 1f
    }

let generateAgent () =
    {
        Genome = [0..8]
                 |> List.map (fun _ -> generateGene ())
    }

let generateWorld () =
    {
        Agents = [0..1000]
                 |> List.mapi (fun _ i -> generateAgent (), i, i)
        Width = 128
        Height = 128
    }

let runEpoch world =
    world

let run callback =
    let world = generateWorld ()

    // TODO
    
    world

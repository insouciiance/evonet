module EvoNet.Mutation

open System
open Agent
open EvoNet.Neurons

let mutateAgent agent =
    let gene = agent.Genome
               |> List.randomChoice
    
    let mutatedGene =
        match Random.Shared.NextSingle() with
        | x when x <= 0.25f -> { gene with Source = randomSource () }
        | x when x <= 0.5f -> { gene with Dest = randomDest ()  }
        | x when x <= 1f -> { gene with Weight = gene.Weight * (Random.Shared.NextSingle() * 4f - 2f) }

    {
        agent with Genome = agent.Genome
                            |> List.map (fun x -> match x with
                                                  | g when g = gene -> mutatedGene
                                                  | g -> g)
    }


let mutate agents =
    agents
    |> List.map (fun x -> match Random.Shared.NextSingle() with
                          | f when f <= 0.001f -> mutateAgent x
                          | _ -> x )

module EvoNet.Crossover

open System
open Agent

let crossoverPair a b =
    {
        Genome = a.Genome @ b.Genome
                 |> Seq.skip (Random.Shared.Next(a.Genome.Length))
                 |> Seq.take a.Genome.Length
                 |> List.ofSeq
    }

let crossover agents =
    agents
    |> Seq.randomShuffle
    |> Seq.pairwise
    |> Seq.map (fun (a, b) -> crossoverPair a b)
    |> List.ofSeq

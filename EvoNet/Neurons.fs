module EvoNet.Neurons

open System
open EvoNet.Neuron

let randomInput () =
    Input (InputNeurons.neurons
    |> Map.keys
    |> Seq.randomChoice)

let randomInternal() =
    Internal (match Random.Shared.NextSingle() with
              | x when x <= 0.5f -> 1uy
              | x when x <= 1f -> 2uy)

let randomOutput () =
    Output (OutputNeurons.neurons
    |> Map.keys
    |> Seq.randomChoice)

let randomSource () =
    match Random.Shared.NextSingle() with
    | x when x <= 0.5f -> randomInput ()
    | x when x <= 1f -> randomInternal ()

let randomDest () =
    match Random.Shared.NextSingle() with
    | x when x <= 0.5f -> randomOutput ()
    | x when x <= 1f -> randomInternal ()

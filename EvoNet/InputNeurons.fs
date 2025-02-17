module EvoNet.InputNeurons

open EvoNet.Neuron
open World

let neurons =
    [
        (1uy, fun a w -> fst (position a w));
        (2uy, fun a w -> snd (position a w));
    ]
    |> Map.ofList
    
let get neuron =
    match neuron with
    | Input x -> neurons
                 |> Map.find x

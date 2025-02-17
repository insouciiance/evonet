module EvoNet.OutputNeurons

open Neuron

open World

let neurons =
    [
        (1uy, fun a w -> move a w (fst (position a w) - 1, (snd (position a w))));
        (2uy, fun a w -> move a w (fst (position a w) + 1, (snd (position a w))));
        (3uy, fun a w -> move a w (fst (position a w), (snd (position a w) - 1)));
        (4uy, fun a w -> move a w (fst (position a w), (snd (position a w) + 1)));
    ]
    |> Map.ofList

let get neuron =
    match neuron with
    | Output x -> neurons
                 |> Map.find x

module EvoNet.Gene

open Neuron

type Gene = {
    Source: Neuron
    Dest : Neuron
    Weight: float32
}

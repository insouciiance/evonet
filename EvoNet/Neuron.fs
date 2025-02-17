module EvoNet.Neuron

type Neuron =
    | Input of id: byte
    | Internal of id: byte
    | Output of id: byte

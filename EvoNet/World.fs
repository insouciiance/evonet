module EvoNet.World

open EvoNet.Agent

type World = {
    Width: int
    Height: int
    Agents: (Agent * int * int) list
}

let position agent world =
    let _, x, y = world.Agents
                |> List.find (fun (a, _, _) -> a = agent)
    
    x, y
    
let move agent world (x, y) =
    let agents = world.Agents
                 |> List.map (fun t -> match t with
                                       | a, _, _ when a = agent -> (agent, x, y)
                                       | _ -> t)
    
    {
        Agents = agents
        Width = world.Width
        Height = world.Height
    }

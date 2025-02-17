module EvoNet.Selection

open World

let select world =
    world.Agents
    |> List.filter (fun (a, x, y) -> x >= world.Width / 2)

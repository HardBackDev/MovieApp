import { Actor } from "../actor-models/actor"

export class MovieWithActors{
    id: string
    directorId: string
    title: string
    description: string
    releaseDate: Date
    genres: string[]
    photoUrl: string
    videoPath: string
    goodRating: number
    goodRatingPercent: number
    badRating: number
    badRatingPercent: number
    actors: Actor[]
}
port module Main exposing (..)

import Browser
import Element exposing (..)
import Html exposing (Html)



-- MAIN


main : Program Int Model Msg
main =
    Browser.document
        { init = init
        , view = view
        , update = update
        , subscriptions = subscriptions
        }


type Msg
    = ReceivedMessage Message


type alias Message =
    { user : String
    , message : String
    }


type alias Model =
    List Message



-- INIT


init : Int -> ( Model, Cmd Msg )
init flags =
    ( [], Cmd.none )



-- UPDATE


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        ReceivedMessage message ->
            ( [ message ] |> List.append model, Cmd.none )



-- VIEW


view : Model -> Browser.Document Msg
view model =
    { title = "The Server Client"
    , body =
        [ layout
            [ width fill
            , height fill
            ]
          <|
            column
                [ centerX
                , centerY
                , spacing 20
                ]
                (model |> List.map (\m -> m.user ++ " " ++ m.message) |> List.map text)
        ]
    }



-- PORTS


port messageReceiver : (Message -> msg) -> Sub msg


subscriptions : Model -> Sub Msg
subscriptions model =
    messageReceiver ReceivedMessage

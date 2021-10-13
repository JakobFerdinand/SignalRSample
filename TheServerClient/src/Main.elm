port module Main exposing (..)

import Browser
import Browser.Dom exposing (Error)
import Element exposing (..)
import Element.Background as Background
import Http
import Json.Decode as Decode



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
    = MessagesLoaded (Result Http.Error (List Message))
    | ReceivedMessage Message


type alias Message =
    { user : String
    , message : String
    }


type Model
    = Loading
    | Loaded (List Message)
    | Error



-- INIT


loadInitialData : Cmd Msg
loadInitialData =
    Http.get
        { url = "http://localhost:8181/api/runners"
        , expect = Http.expectJson MessagesLoaded messagesDecoder
        }


messagesDecoder : Decode.Decoder (List Message)
messagesDecoder =
    Decode.list
        (Decode.map2 Message
            (Decode.field "id" Decode.string)
            (Decode.field "runningTime" Decode.string)
        )


init : Int -> ( Model, Cmd Msg )
init _ =
    ( Loading, loadInitialData )



-- UPDATE


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case ( model, msg ) of
        ( Loading, MessagesLoaded result ) ->
            case result of
                Ok messages ->
                    ( Loaded messages, Cmd.none )

                Err _ ->
                    ( Error, Cmd.none )

        ( Loaded messages, ReceivedMessage message ) ->
            ( Loaded ([ message ] |> List.append messages), Cmd.none )

        ( _, _ ) ->
            ( Error, Cmd.none )



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
                [ width fill
                , height fill
                ]
                [ row
                    [ alignTop
                    , width fill
                    , padding 30
                    , Background.color <| rgb255 200 200 200
                    ]
                    [ el [ centerX ] <| text "SignalR sample with Elm"
                    ]
                , case model of
                    Loading ->
                        el [ centerX, centerY ] <| text "Loading initial data."

                    Loaded messages ->
                        viewMessages messages

                    Error ->
                        el [ centerX, centerY ] <| text "Some error happened."
                ]
        ]
    }


viewMessages : List Message -> Element Msg
viewMessages messages =
    column
        [ centerX
        , height fill
        , spacing 20
        , padding 30
        , scrollbarY
        ]
        (messages |> List.indexedMap (\i -> \m -> String.fromInt (i + 1) ++ " " ++ m.user ++ " " ++ m.message) |> List.map text)



-- PORTS


port messageReceiver : (Message -> msg) -> Sub msg


subscriptions : Model -> Sub Msg
subscriptions model =
    messageReceiver ReceivedMessage
